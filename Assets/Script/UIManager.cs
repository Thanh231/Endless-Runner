using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject dialog;
    public Slider HPSlider;
    public TextMeshProUGUI bullAmountText;

    private void OnEnable()
    {
        EventManager.OnHealthChanged += UpdateHealth;
        EventManager.OnAmmoChanged += UpdateAmmo;
    }
    void OnDisable()
    {
        EventManager.OnHealthChanged -= UpdateHealth;
        EventManager.OnAmmoChanged -= UpdateAmmo;
    }

    private void UpdateAmmo(int arg1, int arg2)
    {
        bullAmountText.text = $"{arg1}/{arg2}";
    }

    private void UpdateHealth(int currentHealth, int maxHealth)
    {
        HPSlider.value = currentHealth / (float)maxHealth;
    }

    private void Awake()
    {
        if (dialog != null)
        {
            dialog?.GetComponent<MenuDialog>().DisplayDialog("ENDLESS RUNNER", "PLAY GAME");
            dialog.SetActive(true);
        }
    }
    public void ShowGameOver()
    {
        StartCoroutine(DelayDisplayGameOverDialog());
    }

    IEnumerator DelayDisplayGameOverDialog()
    {
        yield return new WaitForSeconds(1f);
        dialog?.GetComponent<MenuDialog>().DisplayDialog("YOU DIED", "RETRY");
        dialog?.SetActive(true);
        Time.timeScale = 0f;
    }
}
