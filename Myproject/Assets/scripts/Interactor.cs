using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private InteractionPromptUI _interactionPromptUI;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;
    private IInteractable _interactable;

    //NPC npc;

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if (_numFound > 0)
        {
            Debug.Log("inside num > 0");
            _interactable = _colliders[0].GetComponent<IInteractable>();
            if (_interactable != null)
            {
                Debug.Log("inside interactable not null");
                if (!_interactionPromptUI.IsDisplayed)
                    _interactionPromptUI.SetUp(_interactable.InteractionPrompt);
                if (Keyboard.current.rKey.wasPressedThisFrame)
                    _interactable.Interact(this);
                if (Keyboard.current.lKey.wasPressedThisFrame)
                {
                    Debug.Log("we are in");
                    _interactable.Interact(this);
                }
                
            }
         
        }
        else
        {
            if (_interactable != null) _interactable = null;
            if (_interactionPromptUI.IsDisplayed) _interactionPromptUI.Close();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}