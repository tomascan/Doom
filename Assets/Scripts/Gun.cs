using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public float range = 20f;
    public float verticalRange = 20f;
    public float gunShotRadius = 20f; 
    

    public float smallDamage = 1f;
    public float bigDamage = 2f;


    public float fireRate = 1f;
    private float nextTimeToFire; 
    

    public LayerMask raycastLayerMask;
    public LayerMask enemyLayerMask; 
    
    private BoxCollider gunTrigger;
    public EnemyManager enemyManager;
    void Start()
    {
        gunTrigger = GetComponent<BoxCollider>();
        
        gunTrigger.size = new Vector3(1, verticalRange, range);
        gunTrigger.center = new Vector3(0, 0, range * 0.5f);
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextTimeToFire)
        {
            Fire();
        }
    }

    void Fire()
    {
        
        //Simulate the shotgun radio 
        Collider[] enemyColliders; 
        enemyColliders = Physics.OverlapSphere(transform.position, gunShotRadius, enemyLayerMask);
        
        //Alert enemies in earshot 
        foreach (var enemyCollider in enemyColliders)
        {
            enemyCollider.GetComponent<EnemyAwareness>().isAggro = true; 
        }
        
        //play audio 
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();

        //damage Enemies
        foreach (var enemy in enemyManager.enemiesInTrigger)
        {
            //get direction to enemy 
            var dir = enemy.transform.position - transform.position; 
            
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, range * 1.5f, raycastLayerMask))
            {
                if (hit.transform == enemy.transform)
                {
                    //range check
                    float dist = Vector3.Distance(enemy.transform.position, transform.position);

                    if (dist > range * 0.5f)
                    {
                        enemy.TakeDamage(smallDamage); //Damage the enemy 
                    }
                    else
                    {
                        enemy.TakeDamage(bigDamage); //Damage the enemy 
                    }
                    //Debug.DrawRay(transform.position, dir, Color.green); //Simula el apuntado del arma 
                    //Debug.Break(); //Detiene la ejecución 
                }
            }
        }
        
        //Reset Timer 
        nextTimeToFire = Time.time + fireRate;

    }
    private void OnTriggerEnter(Collider other)
    {
        //add potential enemy to shoot 
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
            enemyManager.AddEnemy(enemy);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {        
        //add potential enemy to shoot 
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if(enemy)
        {
            enemyManager.RemoveEnemy(enemy);
        }
    }
}
