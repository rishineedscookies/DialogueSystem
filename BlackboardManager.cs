using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Singleton manager for blackboards.
 * All blackboard queries should go through this manager
 */
public class BlackboardManager
{

    // List of all blackboards in memory
    public static List<DialogueBlackboard> blackboards = new List<DialogueBlackboard>();

    /*
     * Adds a blackboard to the manager
     */
    public static void AddBlackboard(DialogueBlackboard blackboard)
    {
        blackboards.Add(blackboard);   
    }

    /*
     * Finds the blackboard that contains a given variable name ID
     * Return null if none is found
     */
    public static DialogueBlackboard FindBlackboard(string key)
    {
        foreach(DialogueBlackboard blackboard in blackboards)
        {
            if(blackboard.facts.ContainsKey(key))
            {
                return blackboard;
            }
        }
        return null;
    }

    /*
     * Gets the value of a variable from name ID
     * The key should be check to see if it is valid before getting value
     */
    public static float BlackboardGetValue(string key)
    {
        float value;
        FindBlackboard(key).facts.TryGetValue(key, out value);
        return value;
    }

    /*
     * Sets the initial value of a blackboard variable
     */
    public static void BlackboardSetValue(string key, float value)
    {
        FindBlackboard(key).facts.Add(key, value);
    }

    /*
     * Replace the value of a blackboard variable
     */
    public static void BlackboardReplaceValue(string key, float value)
    {
        FindBlackboard(key).facts[key] = value;
    }

    /*
     * Check to see if an event requirement passes on the managed blackboards
     */
    public static bool Compare(EventRequirement requirement)
    {
        if (requirement == null) return true;
        switch (requirement.comparator)
        {
            case EventRequirementComparator.Is:
                return BlackboardGetValue(requirement.property).Equals(requirement.comparison);
            case EventRequirementComparator.Not:
                return !BlackboardGetValue(requirement.property).Equals(requirement.comparison);
            default:
                return false;
        }
    }

    /*
     * Can a given event be executed?
     */
    public static bool CanExecuteEvent(Event e)
    {
        // True if it passes all requirements
        foreach(EventRequirement requirement in e.requirements)
        {
            if(!Compare(requirement))
            {
                return false;
            }
        }
        return true;
    }

    /*
     * Execute responses to events
     */
    public static void Respond(EventResponse response)
    {
        float value = BlackboardGetValue(response.property);
        switch(response.method)
        {
            case EventResponseMethod.Add:
                value += response.value;
                break;
            case EventResponseMethod.Subtract:
                value -= response.value;
                break;
            case EventResponseMethod.Multiply:
                value *= response.value;
                break;
            case EventResponseMethod.Divide:
                value /= response.value;
                break;
            case EventResponseMethod.Equal:
                value = response.value;
                break;
            default:
                return;
        }
        BlackboardReplaceValue(response.property, value);
    }

}
