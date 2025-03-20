using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100; 
    public int currentHealth; 
    public PlayerHealth healthbar; 
    public Vector3 initialPlayerPosition; 
    public float respawnTime = 3f; 

    void Start()
    {
        isDead = false; 
        currentHealth = maxHealth; 
        healthbar.SetMaxHealth(maxHealth);
        initialPlayerPosition = transform.position;  
    }

    public bool isDead = false; 
    private IEnumerator Respawn(float respawnTime) {
        isDead = true; 
        yield return new WaitForSeconds(respawnTime); 
        Debug.Log("Player has respawned"); 
        currentHealth = maxHealth; 
        healthbar.SetHealth(currentHealth);
        transform.position = initialPlayerPosition; // respawn player at the starting point
        isDead = false; 
    }

    void Update()
    {
        if (currentHealth <= 0 && !isDead) { // check if the player has died first
            Debug.Log("Player is dead"); 
            StartCoroutine(Respawn(respawnTime));
        }
        if (Input.GetKeyDown(KeyCode.H) && !isDead) {
            takeDamage(20); 
        }
    }

    void takeDamage(int damage) {
        currentHealth -= damage; 
        healthbar.SetHealth(currentHealth); 
    }
}
