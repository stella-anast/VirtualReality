using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shutter : MonoBehaviour
{
    public GameObject destroyed; // Object to instantiate when shutter is destroyed
    public KeyCode destroyKey = KeyCode.Z; // Key to destroy the shutter

    // Update is called once per frame
    void Update()
    {
        // Check if the destroy key is pressed
        if (Input.GetKeyDown(destroyKey))
        {
            // Instantiate the destroyed object at the same position and rotation as the shutter
            Instantiate(destroyed, transform.position, transform.rotation);

            // Destroy the shutter object
            Destroy(gameObject);
        }
    }
}
