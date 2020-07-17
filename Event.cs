using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A dialogue event that can be queried
 */
[System.Serializable]
[CreateAssetMenu(menuName = "Events/Event")]
public class Event : ScriptableObject
{

    // Name of this event
    public string name;

    // Source "speaker" of this event
    public string source;
    
    // Target "listener" of this event
    public string target;

    // Dialogue to be spoken
    public string dialogue;

    // Optional next event in sequence
    public Event nextEvent;
    
    // List of required properties for this event
    public List<EventRequirement> requirements;

    // List of responses after the event is run
    public List<EventResponse> responses;

    // List of optional responses that players may pick from
    public string[] options;

    /*
     * Can this event be executed
     */
    public bool CanExecute()
    {
        // Each requirement must pass to be executed
        bool canExecute = true;
        foreach (EventRequirement requirement in requirements)
        {
            if (!BlackboardManager.Compare(requirement))
            {
                return false;
            }
        }
        return canExecute;
    }

}
