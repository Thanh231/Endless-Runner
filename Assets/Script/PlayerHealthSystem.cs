using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    
    public int maxHealth = 100;
    public Animator animator;
    private int currentHealth;
    private void OnEnable()
    {
        EventManager.OnStartGame += SetHPBar;
    }

    private void SetHPBar()
    {
        animator.SetBool("Game Over", false);
        currentHealth = maxHealth;
        EventManager.OnHealthChanged?.Invoke(currentHealth,maxHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        EventManager.OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            animator.SetBool("Game Over", true);
            EventManager.OnPlayerDied?.Invoke();
        }
    }
    
}
