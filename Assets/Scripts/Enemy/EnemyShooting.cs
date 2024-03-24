using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab de la bala que se va a disparar
    public Transform firePoint; // El punto desde donde se dispara la bala
    public Transform target; // Esto debería ser el jugador
    public float fireRate = 1f; // Balas por segundo
    private float nextTimeToFire = 0f;

    void Update()
    {
        // Comprueba si es el momento de disparar nuevamente según la cadencia de fuego
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            AimAndShoot();
        }
    }

    void AimAndShoot()
    {
        if (target != null)
        {
            // Ajusta la orientación del firePoint para que mire hacia el jugador
            firePoint.LookAt(target.position);

            // Crea la bala en el firePoint ya orientado
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Lanza la bala hacia adelante con una velocidad determinada
                rb.velocity = firePoint.forward * 30f; // Ajusta la velocidad según necesites
            }

            // Opcionalmente, destruye la bala después de un tiempo para limpiar la escena
            Destroy(bullet, 2f); // Ajusta este tiempo según necesites
        }
    }
}