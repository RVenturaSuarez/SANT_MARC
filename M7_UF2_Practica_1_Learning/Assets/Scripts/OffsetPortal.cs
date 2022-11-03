using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetPortal : MonoBehaviour
{

    [SerializeField] private float speedOffset;
    [SerializeField] private Material currentMaterial;

    
    void Update()
    {
        float offset = Time.time * speedOffset;
        currentMaterial.SetTextureOffset("_BaseMap", new Vector2(offset,0)); 
    }
}
