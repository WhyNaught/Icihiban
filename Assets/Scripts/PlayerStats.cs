using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100; 
    public int currentHealth; 
    public PlayerHealth healthbar; 
    void Start()
    {
        currentHealth = maxHealth; 
        healthbar.SetMaxHealth(maxHealth); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) {
            takeDamage(20); 
        }
    }

    void takeDamage(int damage) {
        currentHealth -= damage; 
        healthbar.SetHealth(currentHealth); 
    }
}
