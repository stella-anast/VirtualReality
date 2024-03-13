using UnityEngine;

public class pickObject : MonoBehaviour
{
    public GameObject pickObj;
    public GameObject bodyType;
    public Transform parent;

    Vector3 objectOrginalPosition;
    Quaternion objectOrginalRotation;

    private void Start()
    {
        if (pickObj != null)
        {
            objectOrginalPosition = pickObj.transform.position;
            objectOrginalRotation = pickObj.transform.rotation;
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
                pickObj.transform.localPosition = Vector3.zero;
            }
        }
    }
    public void OnDrop()
    {
        if (pickObj != null)
        {
            pickObj.transform.parent = null;
            pickObj.transform.position = objectOrginalPosition;
            pickObj.transform.rotation = objectOrginalRotation;
        }

    }
}