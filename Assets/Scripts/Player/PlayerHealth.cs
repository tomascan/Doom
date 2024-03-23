using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    private int health;
    public int maxArmor;
    private int armor; 

    void Start()
    {
        health = maxHealth;
        CanvasManager.Instance.UpdateHealth(health);
        CanvasManager.Instance.UpdateArmor(armor);
    }

    // Update is called once per frame
    void Update()
    {
        //temporary test function 
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            DamagePlayer(30);
            Debug.Log("Player has been damaged");
        }
    }


    public void DamagePlayer(int damage)
    {
        if (armor > 0)
        {
            if (armor >= damage)
            {
                armor -= damage; 
                CanvasManager.Instance.TriggerDamageEffect();
            }
            else if (armor < damage)
            {
                int remainingDamage = damage - armor;
                armor = 0;
                health -= remainingDamage;
                CanvasManager.Instance.TriggerDamageEffect();
            }
        }
        else
        {
            health -= damage; 
            CanvasManager.Instance.TriggerDamageEffect();
        }

        if (health <= 0)
        {
            Debug.Log("Player Died");

            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }
        CanvasManager.Instance.UpdateHealth(health);
        CanvasManager.Instance.UpdateArmor(armor);
    }


    public void GiveHealth(int amount, GameObject pickup)
    {

        if (health < maxHealth)
        {
            health += amount;
            Destroy(pickup);
        }

        if (health > maxHealth)
        {
            health = maxHealth; 
        }
        
        CanvasManager.Instance.UpdateHealth(health);
    }

    public void GiveArmor(int amount, GameObject pickup)
    {
        if (armor < maxArmor)
        {
            armor += amount;
            Destroy(pickup);
        }
        
        if (armor > maxArmor)
        {
            armor = maxArmor; 
        }
        
        CanvasManager.Instance.UpdateArmor(armor);
    } 
    
}
