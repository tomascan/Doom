using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 1.5f;
    public int attackDamage = 20;
    public float attackCooldown = 1.0f;
    private float lastAttackTime = -1.0f;
    public Transform playerTransform; // Asegúrate de asignar esto desde el editor o encontrarlo en Start()

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);
        if (distanceToPlayer <= attackRange)
        {
            Attack();
        }
    }

    void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            // Aquí es donde realmente atacas al jugador
            // Asumiendo que el jugador tiene un script de salud adjunto a él
            PlayerHealth playerHealth = playerTransform.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
            lastAttackTime = Time.time;
        }
    }
}