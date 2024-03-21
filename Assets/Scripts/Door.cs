using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public Animator doorAnim;

    public bool requiresKey;
    public bool reqRed, reqBlue, reqGreen; 
    
    public GameObject areaToSpawn; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (requiresKey)
            {
                //do additional checks
                if (reqRed && other.GetComponent<PlayerInventory>().hasRed)
                {
                    doorAnim.SetTrigger("OpenDoor");
                    CanvasManager.Instance.ClearKey("red");
                    areaToSpawn.SetActive(true);
                }
                if (reqBlue && other.GetComponent<PlayerInventory>().hasBlue)
                {
                    doorAnim.SetTrigger("OpenDoor");
                    CanvasManager.Instance.ClearKey("blue");
                    areaToSpawn.SetActive(true);
                }
                if (reqGreen && other.GetComponent<PlayerInventory>().hasGreen)
                {
                    doorAnim.SetTrigger("OpenDoor");
                    CanvasManager.Instance.ClearKey("green");
                    areaToSpawn.SetActive(true);
                }
            }
            else
            {
                doorAnim.SetTrigger("OpenDoor");
                areaToSpawn.SetActive(true);
                                
            }

        }
    }
}
