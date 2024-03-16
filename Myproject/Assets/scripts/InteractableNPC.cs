using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;


public class InteractableNPC : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public GameObject player;

    public string InteractionPrompt => _prompt;

    public bool isTalkingWithPlayer;
    public bool playerInRange;


    private void Start()
    {

    }
    public bool Interact(Interactor interactor)
    {
        StartConversation();
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    internal void StartConversation()
    {
        isTalkingWithPlayer = true;

        Debug.Log("conv started");

        DialogSystem.Instance.OpenDialogUI();
        DialogSystem.Instance.dialogText.text = "Hello there";
        DialogSystem.Instance.option1Button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = "Bye";
        DialogSystem.Instance.option1Button.onClick.AddListener(() =>
        {
            DialogSystem.Instance.CloseDialogUI();
            isTalkingWithPlayer = false;
        });
    }



    /* void Update()
     {
         if (npc != null && Keyboard.current.lKey.wasPressedThisFrame && !npc.isTalkingWithPlayer)
         {
             npc.StartConversation();

         }
     }*/
}
