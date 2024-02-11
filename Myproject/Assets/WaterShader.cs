using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

        public Material waterMaterial;


        void Start()
        {

            if (waterMaterial == null)
            {
                Debug.LogError("Water material is not assigned!");
                return;
            }


        }


        void Update()
        {

            float r = Mathf.Sin(Time.time) * 0.5f + 0.5f;
            Color color = new Color(r, 0.0f, 1.0f);
            waterMaterial.SetColor("_Color", color);
        }
    

}
