using UnityEngine;
public class Bullet : MonoBehaviour
{
    public float damage = 10f; // Cantidad de daño que la bala aplica

    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si lo que golpeamos es el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Intentar obtener el script de salud del jugador
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            // Si encontramos el script, aplicamos daño
            if (playerHealth != null)
            {
                playerHealth.TakeDamage((int)damage);
            }

            // Destruye la bala tras el impacto
            Destroy(gameObject);
        }
    }
}