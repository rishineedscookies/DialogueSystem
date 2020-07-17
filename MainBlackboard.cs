using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The MainBlackboard is the primary blackboard for variables that must be maintained through levels
 */
public class MainBlackboard : DialogueBlackboard
{
    void Awake()
    {
        facts = new Dictionary<string, float>();
        // Add blackboard variables here such as
        // facts.Add("Thirst", 100.0f);
        BlackboardManager.AddBlackboard(this);
    }
}
