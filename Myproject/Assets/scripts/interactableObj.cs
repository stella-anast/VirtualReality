using UnityEngine;
using UnityEngine.InputSystem;

public class interactableObj : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public GameObject player;

    public string ItemName;

    public string InteractionPrompt => _prompt;


    public bool Interact(Interactor interactor)
    {
        if (!InventorySystem.Instance.CheckIfFull())
        {
            InventorySystem.Instance.AddToInventory(ItemName);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("The inventory is full");
        }

        return true;
    }

}