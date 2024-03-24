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
    private float nextTimeToFire = 0f;

    public int maxAmmo = 10;
    private int ammo;

    // Usamos una única LayerMask que combine enemigos y misceláneos
    public LayerMask interactableLayerMask;

    private BoxCollider gunTrigger;
    public EnemyManager enemyManager;

    void Start()
    {
        ammo = maxAmmo;
        gunTrigger = GetComponent<BoxCollider>();
        gunTrigger.size = new Vector3(1, verticalRange, range);
        gunTrigger.center = new Vector3(0, 0, range / 2);
        CanvasManager.Instance.UpdateAmmo(ammo);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire && ammo > 0)
        {
            Fire();
        }
    }

    void Fire()
    {
        nextTimeToFire = Time.time + 1f / fireRate;
        ammo--;
        CanvasManager.Instance.UpdateAmmo(ammo);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, gunShotRadius, interactableLayerMask);
        foreach (var hitCollider in hitColliders)
        {
            Enemy enemy = hitCollider.GetComponent<Enemy>();
            Barrel barrel = hitCollider.GetComponent<Barrel>();

            if (enemy != null)
            {
                float dist = Vector3.Distance(hitCollider.transform.position, transform.position);
                float damageAmount = dist > range * 0.5f ? smallDamage : bigDamage;
                enemy.TakeDamage(damageAmount);
            }
            else if (barrel != null)
            {
                // Asumimos que los barriles siempre reciben el "bigDamage" por simplicidad
                barrel.TakeDamage(bigDamage);
            }
        }

        var audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    public void GiveAmmo(int amount, GameObject pickup)
    {
        ammo = Mathf.Min(ammo + amount, maxAmmo);
        Destroy(pickup);
        CanvasManager.Instance.UpdateAmmo(ammo);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemyManager.AddEnemy(enemy);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemyManager.RemoveEnemy(enemy);
            }
        }
    }

    public void ActivateWeaponBoost(float multiplier, float duration)
    {
        StartCoroutine(WeaponBoostCoroutine(multiplier, duration));
    }

    private IEnumerator WeaponBoostCoroutine(float multiplier, float duration)
    {
        fireRate *= multiplier;
        smallDamage *= multiplier;
        bigDamage *= multiplier;

        yield return new WaitForSeconds(duration);

        fireRate /= multiplier;
        smallDamage /= multiplier;
        bigDamage /= multiplier;
    }
}
