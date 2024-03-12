using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;

    private readonly Collider[] _colliders = new Collider[3];

    [SerializeField] private int _numFound;

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius,_colliders, _interactableMask);

        if (_numFound > 0)
        {
            Debug.Log("hello num is greater than 0."); // Add this debug log

            var interactable = _colliders[0].GetComponent<IInteractable>();
            if (interactable != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                //Debug.Log("E key was pressed this frame."); // Add this debug log
                interactable.Interact(this);
            }
            else if (interactable == null)
            {
                Debug.Log("null :(");

            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position,_interactionPointRadius);
    }
}
