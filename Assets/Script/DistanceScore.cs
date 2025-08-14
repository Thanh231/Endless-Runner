using UnityEngine;
using TMPro;
using System;

public class DistanceScore : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject highScoreGO;
    public GameObject newHighScoreGO;

    private Vector3 startPos;
    private float score;
    private float highScore;

    void Start()
    {
        if (player != null)
            startPos = player.position;

        highScore = PlayerPrefs.GetFloat("HighScore", 0);

        if (highScoreText != null)
            highScoreText.text = "High Score: " + Mathf.FloorToInt(highScore) + "m";

        EventManager.ShowHighScore += ShowHighScore;
        EventManager.TurnOffHighScore += TurnOffHighScore;
    }

    private void TurnOffHighScore()
    {
        newHighScoreGO.SetActive(false);
        highScoreGO.SetActive(false);
    }

    void Update()
    {
        if (player != null)
        {
            score = player.position.z - startPos.z;
            score = Mathf.Max(0, score);

            if (scoreText != null)
                scoreText.text = "Score: " + Mathf.FloorToInt(score) + "m";
        }
    }

    public void ShowHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
            PlayerPrefs.Save();
            newHighScoreGO.SetActive(true);
        }

        highScoreGO.SetActive(true);
        if (highScoreText != null)
            highScoreText.text = "High Score: " + Mathf.FloorToInt(highScore) + "m";
    }
}
