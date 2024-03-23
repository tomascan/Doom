using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{
    public float awarenessRadius = 8f; 
    public bool isAggro; 
    public Transform playersTransform;

    private void Start()
    {
        playersTransform = FindObjectOfType<PlayerMove>().transform;
    } 

    private void Update()
    {
        var dist = Vector3.Distance(transform.position, playersTransform.position);
        if (dist < awarenessRadius)
        {
            isAggro = true; 
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