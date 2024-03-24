using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI health; 
    public TextMeshProUGUI armor; 
    public TextMeshProUGUI ammo;

    public Image healthIndicator;

    public Sprite health1; //healthy 
    public Sprite health2; 
    public Sprite health3; 
    public Sprite health4; //dead 

    public GameObject redKey;
    public GameObject blueKey;
    public GameObject greenKey;
    
    
    public Image damageEffectImage; // Asegúrate de asignar esto en el Inspector de Unity
    public float flashSpeed = 5f; // Velocidad a la que desaparecerá el efecto
    private Color flashColor = new Color(1f, 0f, 0f, 0.3f); // Color y transparencia del destello

    public Image powerUpEffectImage; 

    private static CanvasManager _instance;
    

    public static CanvasManager Instance
    {
        get { return _instance; }
    }

    private void Awake()

    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this; 
        }
    }
    
    
    private void Update()
    {
        if (damageEffectImage.color.a > 0)
        {
            damageEffectImage.color = Color.Lerp(damageEffectImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
    }

    // Método para activar el efecto de destello
    public void TriggerDamageEffect()
    {
        damageEffectImage.color = flashColor;
    }
    
    // Método para activar efecto PowerUp
    public void ActivatePowerUpEffect(float duration)
    {
        StartCoroutine(PowerUpEffectCoroutine(duration));
    }

    private IEnumerator PowerUpEffectCoroutine(float duration)
    {
        float startTime = Time.time;
        // Mientras la duración no se haya completado
        while (Time.time - startTime < duration)
        {
            // Alternar la visibilidad del efecto de power-up
            powerUpEffectImage.gameObject.SetActive(!powerUpEffectImage.gameObject.activeSelf);

            // Esperar un breve momento antes de alternar de nuevo
            yield return new WaitForSeconds(0.75f); // Ajusta este valor para controlar la velocidad del parpadeo
        }

        // Asegurar que el efecto se desactiva al finalizar el power-up
        powerUpEffectImage.gameObject.SetActive(false);
    }

    
    
    
    //Methods to upodate the UI
    public void UpdateHealth(int healthValue)
    {
        health.text = healthValue.ToString() + "%";
        UpdateHealthIndicator(healthValue);
    }
    public void UpdateArmor(int armorValue)
    {
        armor.text = armorValue.ToString() + "%";
    }
    public void UpdateAmmo(int ammoValue)
    {
        ammo.text = ammoValue.ToString();
    }
    
    public void UpdateHealthIndicator(int healthValue)
    {
        if (healthValue >= 70)
        {
            healthIndicator.sprite = health1; //Healthy Face
        }
        if (healthValue < 70 && healthValue >= 40)
        {
            healthIndicator.sprite = health2; //Punched Face
        }
        if (healthValue < 40 && healthValue >= 10)
        {
            healthIndicator.sprite = health3; //Bloody Face
        }
        if (healthValue <= 10)
        {
            healthIndicator.sprite = health4; //Dead Face
        }
    }


    public void UpdateKeys(string keyColor)
    {
        if (keyColor == "red")
        {
            redKey.SetActive(true);
        }
        
        if (keyColor == "blue")
        {
            blueKey.SetActive(true);
        }
        
        if (keyColor == "green")
        {
            greenKey.SetActive(true);
        }
    }

    public void ClearKeys()
    {
        redKey.SetActive(false);
        blueKey.SetActive(false);
        greenKey.SetActive(false);
    }
    
    public void ClearKey(string keyColor)
    {
        if (keyColor == "red")
        {
            redKey.SetActive(false);
        }
        
        if (keyColor == "blue")
        {
            blueKey.SetActive(false);
        }
        
        if (keyColor == "green")
        {
            greenKey.SetActive(false);
        }
    }
}
