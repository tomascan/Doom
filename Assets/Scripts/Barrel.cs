using UnityEngine;

public class Barrel : MonoBehaviour
{
    public float health = 1f; // Salud del barril
    public GameObject explosionEffect; // Efecto visual de la explosión
    public float explosionRadius = 5f; // Radio de la explosión
    public LayerMask damageLayer; // Capas que serán afectadas por la explosión
    public float explosionDamage = 25f; // Daño que inflige la explosión

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Explode();
        }
    }

    private void Explode()
    {
        // Instancia el efecto de la explosión
        Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // Detecta objetos dentro del radio de la explosión y aplica daño/empuje
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, damageLayer);
        foreach (var hitCollider in colliders)
        {
            // Aplica daño a los jugadores, otros barriles o enemigos
            if (hitCollider.CompareTag("Player"))
            {
                // Asume que el jugador tiene un script para manejar su salud llamado PlayerHealth
                PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage((int)(explosionDamage));
                }
            }
            else
            {
                // Intenta aplicar daño a otros barriles o enemigos
                var damageable = hitCollider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(explosionDamage);
                }
            }
        }

        // Destruye este barril
        Destroy(gameObject);
    }
}

public interface IDamageable
{
    void TakeDamage(float amount);
}
