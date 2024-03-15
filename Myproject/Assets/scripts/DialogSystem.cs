using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    public static DialogSystem Instance { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
           
        }
        else
        {
            Instance = this;
        }
    }
}
