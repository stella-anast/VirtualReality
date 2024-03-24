using UnityEngine;

public class Door : MonoBehaviour
{
    public string requiredKey;
    public GameObject opened;
    public Vector3 openedDoorPosition;
    public Quaternion openedDoorRotation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (InventorySystem.Instance.HasItem(requiredKey))
            {
                OpenDoor();
            }
        }
    }

    private void OpenDoor()
    {
        Instantiate(opened, openedDoorPosition, openedDoorRotation);
        Destroy(gameObject);
    }
}
