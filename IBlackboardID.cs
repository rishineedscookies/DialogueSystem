using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Identification system for blackboard variables
 */
public interface IBlackboardID
{

    // Gets the value of this variable
    dynamic GetValue();

    // Sets the value of this variable
    void SetValue(dynamic value);

    // Returns if the value of this variable equals a given value
    bool Equals(dynamic value);
    
    // Returns how the value of this variable compares with a given value and comparator
    bool CompareTo(dynamic value, EventRequirementComparator comparator);

}
