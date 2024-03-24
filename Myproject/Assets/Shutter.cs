using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shutter : MonoBehaviour
{
    public KeyCode shutter = KeyCode.O;
    public GameObject destroyed;
    //method to destroy chest
    void Update()
    {
        
        if (Input.GetKeyDown(shutter))
        {
            Instantiate(destroyed,transform.position,transform.rotation);
            Destroy(gameObject);
        }
    }
}
