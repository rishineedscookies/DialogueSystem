# DialogueSystem

This Unity3D dialogue system is a unique blackboard-queried event-based dialogue system whereby the system find the most appropriate and specific dialogue to run by queries a series of dialogues to find the one that passes the most requirements.
This is unique in that is allows for a more natural way of writing dialogue that can be used to reference past player actions, current player actions (for example what the player is looking at), and other properties of the game.
This system can be further developed to become a full event system.

TODO:
- Implement robust blackboard variable identification system
- UI for managing dialogue event system
- Serialization of all dialogue events
- Memory loading and unloading of blackboards
- Priority queue sorted event list to optimize query times
- Multithreaded queries
