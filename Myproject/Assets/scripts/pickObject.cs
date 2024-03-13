using UnityEngine;

public class pickObject : MonoBehaviour
{
    public GameObject pickObj;
    public GameObject bodyType;
    public Transform parent; // The hand transform

    Vector3 objectOriginalPosition;
    Quaternion objectOriginalRotation;

    private void Start()
    {
        if (pickObj != null)
        {
            objectOriginalPosition = pickObj.transform.position;
            objectOriginalRotation = pickObj.transform.rotation;
        }
    }

    public void OnPickUp()
    {
        if (pickObj != null)
        {
            Vector3 distance = pickObj.transform.position - bodyType.transform.position;
            float magnitude = distance.magnitude;
            Debug.Log(magnitude);

            if (magnitude <= 2f)
            {
                pickObj.transform.SetParent(parent);

                // Reset local position before setting it
                pickObj.transform.localPosition = Vector3.zero;

                // Calculate the desired rotation
                Quaternion targetRotation = parent.rotation * Quaternion.Euler(0f, 180f, 0f); // Assuming hand's up direction is along the y-axis, adjust 180 degrees if necessary

                // Set object's rotation
                pickObj.transform.rotation = targetRotation;
            }
        }
    }




    public void OnDrop()
    {
        if (pickObj != null)
        {
            // Reset parent to null
            pickObj.transform.parent = null;
            // Reset position and rotation
            pickObj.transform.position = objectOriginalPosition;
            pickObj.transform.rotation = objectOriginalRotation;
        }
    }
}
