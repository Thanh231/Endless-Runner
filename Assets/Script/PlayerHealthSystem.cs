using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    
    public int maxHealth = 100;
    public Animator animator;
    private int currentHealth;
    public int currentLive;
    public int maxiLive = 3;
    public GameObject liveUI;

    public TextMeshProUGUI liveText;

    private void OnEnable()
    {
        EventManager.OnStartGame += SetHPBar;
        currentLive = maxiLive;
        SetHPBar();
    }

    private void SetHPBar()
    {
        liveUI.SetActive(true);
        liveText.text = "Live:" + currentLive.ToString();
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
            AudioController.Ins.PlaySound(AudioController.Ins.playerDeath, AudioController.Ins.sfxAus);
            currentLive--;
            if (currentLive > 0)
            {
                EventManager.OnPlayerDied?.Invoke();
            }
            else
            {
                currentLive = maxiLive;
                EventManager.OnGameOver?.Invoke();
            }
            liveUI.SetActive(false);

        }
    }
    
}
