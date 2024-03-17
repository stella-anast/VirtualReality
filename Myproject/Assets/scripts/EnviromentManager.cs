﻿using UnityEngine;
using System.Collections;

public class EnviromentManager : MonoBehaviour
{
    public static EnviromentManager Instance { get; set; }

    public GameObject allItems;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}

