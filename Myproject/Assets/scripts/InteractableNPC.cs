using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InteractableNPC : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public GameObject player;

    public string InteractionPrompt => _prompt;

    NPC npc;

    private void Start()
    {

    }
    public bool Interact(Interactor interactor)
    {
        npc.StartConversation();

        return true;
    }

    /* void Update()
     {
         if (npc != null && Keyboard.current.lKey.wasPressedThisFrame && !npc.isTalkingWithPlayer)
         {
             npc.StartConversation();

         }
     }*/
}
