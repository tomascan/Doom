using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public bool hasRed, hasBlue, hasGreen;

    private void Start()
    {
        CanvasManager.Instance.ClearKeys();
        
    }
}
