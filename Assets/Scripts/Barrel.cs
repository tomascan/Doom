using UnityEngine;

public class Barrel : MonoBehaviour
{
    public GameObject explosionEffect;
    public float explosionRadius = 5f;
    public int damage = 50;
    public float enemyHealth = 2f;
    public GameObject itemToDrop;
    public GameObject gunHitEffect;

    void Update()
    {
        if (enemyHealth <= 0)
        {
            Explode();
        }
    }

    public void TakeDamage(float damage)
    {
        Instantiate(gunHitEffect, transform.position, Quaternion.identity);
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            Explode();
        }
    }

    public void Explode()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        ApplyDamageToNearbyObjects();
        DropItem();
        Destroy(this);
    }

    private void ApplyDamageToNearbyObjects()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hitCollider in colliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.DamagePlayer(damage);
                }
            }

            // Aquí podrías añadir daño a otros enemigos si lo necesitas
            // Similar al daño al jugador, pero asegúrate de que cada enemigo tiene su propio componente de salud
        }
    }

    private void DropItem()
    {
        if (itemToDrop != null)
        {
            Instantiate(itemToDrop, transform.position, Quaternion.identity);
        }
    }
}