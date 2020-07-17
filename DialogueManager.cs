using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Singleton manager for dialogue. Controls the transition to and from events and event systems
 */
public class DialogueManager : MonoBehaviour
{
    // Singleton instance of this
    public static DialogueManager instance;

    // The current event system loaded
    public EventSystem currentSystem;

    // The current event being played
    Event currentEvent;

    // Is the player in a conversation?
    public bool inConversation = false;
    
    // Delegate for changes in conversation states
    public delegate void OnConversationChange();
    
    // Delegate for changes to player choices
    public delegate void OnChoicesChange(string[] text);

    // Event run when a new conversation starts
    public static OnConversationChange onConversationStart;

    // Event run when new choices are presented
    public static OnChoicesChange onChoicesStart;

    // Event run when choices stop
    public static OnConversationChange onConversationChoicesEnd;

    // Event run when a conversation ends
    public static OnConversationChange onConversationEnd;

    // Setup singleton behavior
    void Start()
    {
        instance = this;
        onConversationEnd += ClearEvent;
    }

    /*
     * Updates the current event system to a new one
     */
    public void UpdateSystem(EventSystem eventSystem)
    {
        currentSystem = eventSystem;
        currentEvent = null;
        // Loads the next event of the new system if we can
        if(LoadNextEvent())
        {
            onConversationStart();
        }
    }

    /*
     * Loads the current event
     */
    public void LoadEvent()
    {
        if(currentEvent != null)
        {
            UIManager.instance.UpdateDialogue(currentEvent.dialogue);
            if(currentEvent.options.Length > 0)
            {
                string[] text = currentEvent.options;
                onChoicesStart(text);
            }
            else
            {
                onConversationChoicesEnd();
            }
            instance.inConversation = true;
            ExecuteResponses();
        } 
    }

    /*
     * Loads the next queried event or the next event in sequence
     */
    public bool LoadNextEvent()
    {
        // If we are not in an event then try loading the next event in the system
        if(currentEvent == null)
        {
            currentEvent = currentSystem.GetNextEvent();
            // If there are no more next events then end the conversation
            if (currentEvent == null)
            {
                onConversationEnd();
                instance.inConversation = false;
                Debug.Log("Conversation Ended");
                return false;
            }
            else
            {
                // Load the new event
                LoadEvent();
            }
        }
        else
        {
            // See if there is a sequenced event
            currentEvent = currentEvent.nextEvent;
            if (currentEvent == null)
            {
                // If we don't find a sequenced event, query for one instead
                LoadNextEvent();
            }
            else
            {
                // Load the sequenced event
                LoadEvent();
            }
        }
        return true;
    }

    /*
     * Executes all responses of the event that was just finished
     */
    public void ExecuteResponses()
    {
        if(currentEvent != null)
        {
            foreach(EventResponse response in currentEvent.responses)
            {
                BlackboardManager.Respond(response);
            }
        }

    }

    /*
     * Loads all options from this event
     */
    private void LoadOptionEvent(Event option)
    {
        currentEvent = option;
        LoadEvent();
    }

    /*
     * Choose an option from this event
     */
    public void ChooseOption(int optionID)
    {
        if(optionID <= currentEvent.options.Length)
        {
            LoadOptionEvent(currentEvent.options[optionID - 1]);
        }
    }

    /*
     * Clears the event and event system
     */
    public void ClearEvent()
    {
        currentSystem = null;
        currentEvent = null;
    }

}
