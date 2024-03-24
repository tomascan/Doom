using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab de la bala que se va a disparar
    public Transform firePoint; // El punto desde donde se dispara la bala
    public float fireRate = 1f; // Balas por segundo
    private float nextTimeToFire = 0f;

    void Update()
    {
        // Considera añadir una condición para comprobar si el jugador está en rango
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        // Crear una nueva bala desde el prefab
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        // Aquí puedes añadir más lógica, por ejemplo para darle una velocidad inicial a la bala
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * 20f; // Reemplaza 20f con la velocidad que desees
        }
        
        // Opcionalmente, puedes destruir la bala después de un tiempo si no choca con nada
        Destroy(bullet, 2f); // Reemplaza 5f con la cantidad de segundos antes de destruir la bala automáticamente
    }
}