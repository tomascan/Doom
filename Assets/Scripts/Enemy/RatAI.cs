using UnityEngine;
using UnityEngine.AI;

public class RatAI : MonoBehaviour
{
    public NavMeshAgent agent; // El componente NavMeshAgent de la rata
    public Transform player; // La transformada del jugador
    public float detectionRadius = 10f; // Radio en el que la rata detectará al jugador

    private void Start()
    {
        // Obtener el componente NavMeshAgent
        agent = GetComponent<NavMeshAgent>();

        // Encuentra el objeto del jugador en la escena y asigna su transformada
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Verifica si el jugador está dentro del radio de detección
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRadius)
        {
            FleeFromPlayer();
        }
    }

    private void FleeFromPlayer()
    {
        // Calcula la dirección para huir del jugador
        Vector3 fleeDirection = transform.position - player.position;

        // El punto para huir será en la dirección opuesta al jugador
        Vector3 fleePosition = transform.position + fleeDirection.normalized * detectionRadius;

        // Asegúrate de que el destino de huida esté dentro del NavMesh
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(fleePosition, out navHit, detectionRadius, NavMesh.AllAreas))
        {
            // Establece el nuevo destino de huida en el NavMeshAgent
            agent.SetDestination(navHit.position);
        }
    }
}