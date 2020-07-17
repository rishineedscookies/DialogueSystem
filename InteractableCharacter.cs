using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Defines characters that the player can interact with
 */
public class InteractableCharacter : ViewableObjectController
{

    // EventSystem for all dialogue or other events
    public EventSystem eventSystem;

    /*
     * Interact with this player
     */
    public void Interact()
    {
        DialogueManager.instance.UpdateSystem(eventSystem);
    }
}
