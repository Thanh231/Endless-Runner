using UnityEngine;
using TMPro;

public class DistanceScore : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject highScoreGO;

    private Vector3 startPos;
    private float score;
    private float highScore;

    void Start()
    {
        PlayerPrefs.SetFloat("HighScore", 0);
        if (player != null)
            startPos = player.position;

        highScore = PlayerPrefs.GetFloat("HighScore", 0);

        if (highScoreText != null)
            highScoreText.text = "High Score: " + Mathf.FloorToInt(highScore) + "m";

        EventManager.ShowHighScore += ShowHighScore;
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

    public void ShowHighScore(bool isShow = false)
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
            PlayerPrefs.Save();
        }


        if (isShow)
        {
            highScoreGO.SetActive(true);
            if (highScoreText != null)
                highScoreText.text = "High Score: " + Mathf.FloorToInt(highScore) + "m";
        }
        else
        {
            highScoreGO.SetActive(false);
        }
    }
}
