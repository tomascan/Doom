using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{


    public float awarenessRadius = 8f; 
    public bool isAggro; 
    public Material aggroMat;
    public Transform playersTransform;

    private void Start()
    {
        playersTransform = FindObjectOfType<PlayerMove>().transform;
    } 

    // Update is called once per frame
    private void Update()
    {
        var dist = Vector3.Distance(transform.position, playersTransform.position);
        if (dist < awarenessRadius)
        {
            isAggro = true; 
        }

        if (isAggro)
        {
            GetComponent<MeshRenderer>().material = aggroMat;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            isAggro = true;
        }
    }


}
