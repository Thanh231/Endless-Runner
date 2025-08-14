using System.Collections;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{

    public int maxHealth = 100;
    public Animator animator;
    private int currentHealth;

    public DistanceScore score;
    private void OnEnable()
    {
        EventManager.OnStartGame += SetHPBar;
        SetHPBar();
    }

    private void SetHPBar()
    {
        animator.SetBool("Game Over", false);
        currentHealth = maxHealth;
        EventManager.OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        EventManager.OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            animator.SetBool("Game Over", true);
            StartCoroutine(WaitDeathAnimComplete());
            EventManager.OnStopGame?.Invoke();
        }
    }

    IEnumerator WaitDeathAnimComplete()
    {
        yield return new WaitForSeconds(1f);
        AudioController.Ins.PlaySound(AudioController.Ins.playerDeath, AudioController.Ins.sfxAus);
        EventManager.OnGameOver?.Invoke();
        Time.timeScale = 0f;
    }
}
