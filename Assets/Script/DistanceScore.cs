using UnityEngine;
using TMPro;

public class DistanceScore : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private Vector3 startPos;
    private float score;
    private float highScore;

    void Start()
    {
        if (player != null)
            startPos = player.position;

        highScore = PlayerPrefs.GetFloat("HighScore", 0);

        if (highScoreText != null)
            highScoreText.text = "High Score: " + Mathf.FloorToInt(highScore);
    }

    void Update()
    {
        if (player != null)
        {
            score = player.position.z - startPos.z;
            score = Mathf.Max(0, score);

            if (scoreText != null)
                scoreText.text = "Score: " + Mathf.FloorToInt(score);
        }
    }

    public void ShowHigScore()
    {
        
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
            PlayerPrefs.Save();

            if (highScoreText != null)
            highScoreText.text = "High Score: " + Mathf.FloorToInt(highScore);
        }
    }
}
