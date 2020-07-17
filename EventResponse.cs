using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enum for event responses
public enum EventResponseMethod
{
    Add,
    Subtract,
    Multiply,
    Divide,
    Equal
}

/*
 * Data for event responses to control blackboard variables
 */
[CreateAssetMenu(menuName = "Events/Event Response")]
[System.Serializable]
public class EventResponse : ScriptableObject
{
    // The name ID of the variable being changed
    public string property;

    // The response method of this response
    public EventResponseMethod method;

    // The value to response with
    public float value;
}
