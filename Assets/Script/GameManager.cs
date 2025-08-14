
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private void OnEnable()
    {
        Time.timeScale = 0f;
        EventManager.OnGameOver += ResetGame;
    }

    private void ResetGame()
    {
        UIManager.Ins.ShowGameOver();
    }

    private void OnDisable()
    {
        EventManager.OnGameOver -= ResetGame;
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        StartCoroutine(DelayStartGame());
    }

    private IEnumerator DelayStartGame()
    {
        yield return new WaitForSeconds(0.3f);
        EventManager.OnStartGame?.Invoke();
    }
}
