using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public float range = 10f;
    public float verticalRange = 5f;
    public float gunShotRadius = 5f; 
    

    public float smallDamage = 1f;
    public float bigDamage = 2f;


    public float fireRate = 1f;
    private float nextTimeToFire;

    public int maxAmmo = 10;
    private int ammo; 
    

    public LayerMask raycastLayerMask;
    public LayerMask enemyLayerMask; 
    
    private BoxCollider gunTrigger;
    public EnemyManager enemyManager;
    void Start()
    {
        gunTrigger = GetComponent<BoxCollider>();
        
        gunTrigger.size = new Vector3(1, verticalRange, range);
        gunTrigger.center = new Vector3(0, 0, range * 0.5f);
        
        CanvasManager.Instance.UpdateAmmo(ammo);
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextTimeToFire && ammo > 0)
        {
            Fire();
        }
    }

    void Fire()
    {
        // Simulate the shotgun radio
        Collider[] enemyColliders;
        enemyColliders = Physics.OverlapSphere(transform.position, gunShotRadius, enemyLayerMask);

        // Alert enemies in earshot
        foreach (var enemyCollider in enemyColliders)
        {
            var enemyAwareness = enemyCollider.GetComponent<EnemyAwareness>();
            if (enemyAwareness != null)
            {
                enemyAwareness.isAggro = true;
            }
        }

        // Play audio
        var audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.Play();
        }

        // Damage Enemies
        List<Enemy> enemiesToDamage = new List<Enemy>(enemyManager.enemiesInTrigger);
        foreach (var enemy in enemiesToDamage)
        {
            // Get direction to enemy
            var dir = enemy.transform.position - transform.position;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, range * 1.5f, raycastLayerMask))
            {
                if (hit.transform == enemy.transform)
                {
                    // Range check
                    float dist = Vector3.Distance(enemy.transform.position, transform.position);

                    if (dist > range * 0.5f)
                    {
                        enemy.TakeDamage(smallDamage); // Damage the enemy
                    }
                    else
                    {
                        enemy.TakeDamage(bigDamage); // Damage the enemy
                    }
                }
            }
        }

        // Reset Timer
        nextTimeToFire = Time.time + fireRate;

        // Reduce ammo
        ammo--;
        CanvasManager.Instance.UpdateAmmo(ammo);
    }



    public void GiveAmmo(int amount, GameObject pickup)
    {
        if (ammo < maxAmmo)
        {
            ammo += amount;
            Destroy(pickup);
        }

        if (ammo > maxAmmo)
        {
            ammo = maxAmmo;
        }
        
        CanvasManager.Instance.UpdateAmmo(ammo);
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
    
    // PowerUp en Damage 
    public void ActivateWeaponBoost(float multiplier, float duration)
    {
        StartCoroutine(WeaponBoostCoroutine(multiplier, duration));
    }

    private IEnumerator WeaponBoostCoroutine(float multiplier, float duration)
    {
        range *= multiplier;
        smallDamage *= multiplier;
        bigDamage *= multiplier;

        yield return new WaitForSeconds(duration);

        range /= multiplier;
        smallDamage /= multiplier;
        bigDamage /= multiplier;
    }

}
