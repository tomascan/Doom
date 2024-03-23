using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float shootingRange = 10f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 20f;
    public float shootingCooldown = 2f;

    private float shootingTimer;

    void Update()
    {
        if (shootingTimer > 0)
        {
            shootingTimer -= Time.deltaTime;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && shootingTimer <= 0)
        {
            ShootAtPlayer(other.transform.position);
            shootingTimer = shootingCooldown;
        }
    }

    void ShootAtPlayer(Vector3 playerPosition)
    {
        Vector3 directionToPlayer = (playerPosition - bulletSpawnPoint.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(directionToPlayer));
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = directionToPlayer * bulletSpeed;
    }


}