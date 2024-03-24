using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime); // Destruye la bala después de un tiempo para no sobrecargar la escena
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other) // Asegúrate de que 'Collider' aquí esté reconocido correctamente por Unity
    {
        if (other.CompareTag("Player"))
        {
            // Asume que tienes un componente PlayerHealth en tu jugador
            other.GetComponent<PlayerHealth>().DamagePlayer(damage);
        }
        Destroy(gameObject); // Destruye la bala al colisionar
    }
}