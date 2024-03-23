using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shutter : MonoBehaviour
{
    public KeyCode shutter = KeyCode.O;
    public GameObject destroyed;
    void Update()
    {
        // Check if the designated button is pressed
        if (Input.GetKeyDown(shutter))
        {
            Instantiate(destroyed,transform.position,transform.rotation);
            Destroy(gameObject);
        }
    }
}
