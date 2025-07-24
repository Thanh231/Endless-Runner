using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth = 3;
    public Slider HPSlider;
    public void SetHealth()
    {
        if (HPSlider != null)
        {
            currentHealth = maxHealth;
            UpdateHealth(currentHealth, maxHealth);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var healthSystem = other.GetComponent<PlayerHealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.TakeDamage(20);
            }
            SpawnerManager.Ins.AddStack(this.gameObject);
        }
    }

    private void UpdateHealth(int currentHealth, int maxHealth)
    {
        HPSlider.value = currentHealth/(float)maxHealth;
    }

    public void TakeDamage()
    {
        currentHealth--;
        UpdateHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            SpawnerManager.Ins.AddStack(this.gameObject);
        }
    } 
}
