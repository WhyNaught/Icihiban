using UnityEngine;
using UnityEngine.UI; 

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider; // slider UI component

    public void SetMaxHealth(int maxHealth) {
        healthSlider.maxValue = maxHealth; 
        healthSlider.value = maxHealth; 
    }

    public void SetHealth(int health) {
        healthSlider.value = health; 
    }
}
