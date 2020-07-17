using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Blackboard for dialogue.
 * Holds properties about variables which can be used to query dialogue events
 */
public class DialogueBlackboard : MonoBehaviour
{

    // Dictionary of blackboard variables to their values
    public Dictionary<string, float> facts;

    // Add this blackboard to the manager
    private void Start()
    {
        BlackboardManager.AddBlackboard(this);
    }

    /*
     * Returns if a variable in this blackboard passes an event requirement
     */
    public bool Compare(EventRequirement requirement)
    {
        // Get the value that the requirement wants
        float value;
        facts.TryGetValue(requirement.property, out value);
        // Check to see if variable passes requirement
        switch (requirement.comparator)
        {
            case EventRequirementComparator.Is:
                return value.Equals(requirement.comparison);
            case EventRequirementComparator.Not:
                return !facts.TryGetValue(requirement.property, out value).Equals(requirement.comparison);
                break;
            default:
                return false;
        }
    }



}
