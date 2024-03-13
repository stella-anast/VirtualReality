using UnityEngine;

public class Bottle : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public GameObject player;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Interact method called on Bottle.");
        _animator.Play("Drinking"); 
        return true;
    }
}
