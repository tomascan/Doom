using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyManager enemyManager;
    private Animator spriteAnim;
    private AngleToPlayer angleToPlayer;

    public float enemyHealth = 2f;
    public GameObject itemToDrop; // Asigna el objeto a soltar en el Inspector
    public GameObject gunHitEffect;

    private void Start()
    {
        spriteAnim = GetComponentInChildren<Animator>();
        angleToPlayer = GetComponent<AngleToPlayer>();
        enemyManager = FindObjectOfType<EnemyManager>(); // Considera usar una referencia directa en vez de FindObjectOfType si es posible para mejorar el rendimiento.
    } 

    void Update()
    {
        spriteAnim.SetFloat("spriteRot", angleToPlayer.lastIndex);

        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        Instantiate(gunHitEffect, transform.position, Quaternion.identity);
        enemyHealth -= damage;
        if(enemyHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        DropItem();
        enemyManager.RemoveEnemy(this);
        Destroy(gameObject);
    }

    private void DropItem()
    {
        if (itemToDrop != null)
        {
            Instantiate(itemToDrop, transform.position, Quaternion.identity);
        }
    }
}