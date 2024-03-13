using UnityEngine;

public class Bottle : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private Animator _animator; // Reference to the Animator component

    // Index of the layer you want to animate
    [SerializeField] private int _layerIndex = 2;

    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        if (_animator != null)
        {
            // Trigger the "PickUp" animation in the specified layer
            _animator.Play("PickUp", _layerIndex);
        }
        else
        {
            Debug.LogWarning("Animator reference not set in Bottle script.");
        }

        return true;
    }
}
