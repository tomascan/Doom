using UnityEngine;

public class Barrel : MonoBehaviour
{
    public GameObject explosionEffect; // Prefab de la explosión visual
    public float explosionRadius = 5f; // Radio de la explosión
    public float explosionForce = 700f; // Fuerza de la explosión
    public int damage = 50; // Daño que aplica la explosión

    // Esta función se llamará cuando el barril deba explotar
    public void Explode()
    {
        // Muestra la animación de explosión o efecto de partículas
        Instantiate(explosionEffect, transform.position, transform.rotation);

        // Detecta al jugador usando Physics.OverlapSphere y aplica daño
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in colliders)
        {
            // Aquí podrías comprobar si el collider pertenece al jugador
            if (hitCollider.CompareTag("Player"))
            {
                // Aplica daño al jugador
                PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.DamagePlayer(damage);
                }

                // Aplica una fuerza de explosión si el jugador tiene un Rigidbody
                Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }
        }

        // Finalmente, destruye el barril
        Destroy(gameObject);
    }

    // Resto del código...
}