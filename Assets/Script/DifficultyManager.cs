
using TMPro;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public Player player;
    public float timeToIncrease = 10f;
    public float speedIncrease = 1f;
    private float timer = 0f;
    [SerializeField] private TextMeshProUGUI speedText;

    private void Start()
    {
        UpdateSpeedText();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToIncrease)
        {
            player.speedTranslate += speedIncrease;
            UpdateSpeedText();
            timer = 0f;
        }
    }

    void UpdateSpeedText() {
    speedText.text = "SPEED: " + player.speedTranslate.ToString("F1");
}
}
