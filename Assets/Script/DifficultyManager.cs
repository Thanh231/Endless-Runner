
using TMPro;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public Player player;
    public float timeToIncrease = 10f;
    public float speedIncrease = 1f;
    public float currentSpeed = 1f;
    public float maxSpeed = 11f;
    private float timer = 0f;

    private void Start()
    {
        // UpdateSpeedText();
        currentSpeed = speedIncrease;
    }
    private void OnEnable()
    {
        EventManager.OnGameOver += OnResetGame;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToIncrease && player.speedTranslate <= maxSpeed)
        {
            player.speedTranslate += speedIncrease;
            // UpdateSpeedText();
            timer = 0f;
        }
        if (player.speedTranslate == 11f)
        {
            SpawnerManager.Ins.isLevelUp = true;
        }
    }

    public void OnResetGame()
    {
        currentSpeed = speedIncrease;
        player.speedTranslate = speedIncrease;
    }
}
