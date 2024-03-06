using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWave : MonoBehaviour
{
    void Start(){

    }

    void Update(){

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

        }
        other.gameObject.SetActive(false);
    }
}