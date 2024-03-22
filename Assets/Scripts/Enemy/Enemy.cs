using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{

    private EnemyManager enemyManager;
    private Animator spriteAnim;
    private AngleToPlayer angleToPlayer;
    
    
    private float enemyHealth = 2f;


    public GameObject gunHitEffect;


    private void Start()
    {
        spriteAnim = GetComponentInChildren<Animator>();
        angleToPlayer = GetComponent<AngleToPlayer>();

        enemyManager = FindObjectOfType<EnemyManager>(); //We  can do this cause theres only 1 in the scene...
    } 

    // Update is called once per frame
    void Update()
    {
        //beginning of update set the animations rotational index 
        spriteAnim.SetFloat("spriteRot",angleToPlayer.lastIndex);
        
        if (enemyHealth <= 0)
        {
            enemyManager.RemoveEnemy(this);
            Destroy(gameObject);
        }    
        
        //any animations we call will have updated index 
    }

    public void TakeDamage(float damage)
    {
        Instantiate(gunHitEffect, transform.position, Quaternion.identity);
        enemyHealth -= damage;
    }
}
