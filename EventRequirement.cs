using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Possible blackboard variable comparators
public enum EventRequirementComparator
{
    Is,
    Not
}

/*
 * A requirement for an event to execute
 */
[System.Serializable]
[CreateAssetMenu(menuName = "Events/Event Requirement")]
public class EventRequirement : ScriptableObject {

    // Blackboard variable name ID that must have some property
    public string property;
    
    // Comparator for some blackboard variable
    public EventRequirementComparator comparator;

    // Value to compare against
    public float comparison;

}
