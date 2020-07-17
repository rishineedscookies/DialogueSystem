using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * An event system that queries a sequence of events
 */ 
[System.Serializable]
[CreateAssetMenu(menuName = "Events/Event System")]
public class EventSystem : ScriptableObject
{

    // The list of all events in this system
    public List<Event> events;

    // Returns the next event according to the query
    public Event GetNextEvent()
    {
        // Find the event that passes the most requirements
        // This is the event that is the most specific and "appropriate"
        int maxRequirements = 0;
        Event bestEvent = null;
        foreach(Event e in events)
        {
            if(e.CanExecute() && e.requirements.Count >= maxRequirements)
            {
                maxRequirements = e.requirements.Count;
                bestEvent = e;
            }
        }

        return bestEvent;
    }

}
