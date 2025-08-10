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
            dialog?.GetComponent<MenuDialog>().DisplayDialog("ENDLESS RUNNER", "PLAY GAME",false);
            dialog.SetActive(true);
        }
    }
    public void ShowRetryUI()
    {
        StartCoroutine(DelayDisplayGameOverDialog("YOU DIED", "RETRY"));
    }

    public void ShowGameOver()
    {
        dialog?.GetComponent<MenuDialog>().DisplayDialog("YOU LOSE", "RESTART",true);
        dialog?.SetActive(true);
        Time.timeScale = 0f;
    }

    IEnumerator DelayDisplayGameOverDialog(string title,string buttonText,bool isShow = false)
    {
        yield return new WaitForSeconds(1f);
        dialog?.GetComponent<MenuDialog>().DisplayDialog(title,buttonText,isShow);
        dialog?.SetActive(true);
        Time.timeScale = 0f;
    }
}
