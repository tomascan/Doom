using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{


    public bool isHealth; //Health Flag

    public bool isArmor; //Armor Flag

    public bool isAmmo; //Ammo Flag
    
    public bool isPowerUp; // Flag para el power-up general
    public float powerUpDuration = 10f; // Duración del power-up


    public int amount; 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isHealth)
            {
                other.GetComponent<PlayerHealth>().GiveHealth(amount, this.gameObject);
            }
            if (isArmor)
            {
                other.GetComponent<PlayerHealth>().GiveArmor(amount, this.gameObject);
            }
            if (isAmmo)
            {
                other.GetComponentInChildren<Gun>().GiveAmmo(amount, this.gameObject);
            }
            if (isPowerUp) // Si es el power-up general
            {
                // Activar el power-up de velocidad
                other.GetComponent<PlayerMove>().ActivateSpeedBoost(2f, powerUpDuration);

                // Activar el power-up de daño y alcance
                other.GetComponentInChildren<Gun>().ActivateWeaponBoost(3f, powerUpDuration);

                // Activar el efecto visual en el HUD
                CanvasManager.Instance.ActivatePowerUpEffect(powerUpDuration);

                // Destruir el objeto power-up después de la recogida
                Destroy(gameObject);
            }
        }
    }
}
