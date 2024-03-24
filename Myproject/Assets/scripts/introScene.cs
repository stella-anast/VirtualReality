using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introScene : MonoBehaviour
{
    public float changeTime;
    [SerializeField] GameObject inScreen;
    // Update is called once per frame
    void Update()
    {
        changeTime -= Time.deltaTime;
        if( changeTime < 0)
        {
            inScreen.SetActive(false);
        }
    }
}
