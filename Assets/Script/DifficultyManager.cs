
using TMPro;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public Player player;
    public float timeToIncrease = 10f;
    public float speedIncrease = 1f;
    public float maxSpeed = 11f;
    private float timer = 0f;
    [SerializeField] private TextMeshProUGUI speedText;

    private void Start()
    {
        UpdateSpeedText();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToIncrease && player.speedTranslate <= maxSpeed)
        {
            player.speedTranslate += speedIncrease;
            UpdateSpeedText();
            timer = 0f;
        }
        if (player.speedTranslate == 11f)
        {
            SpawnerManager.Ins.isLevelUp = true;
        }
    }

    void UpdateSpeedText() {
    speedText.text = "SPEED: " + player.speedTranslate.ToString("F1");
}
}
