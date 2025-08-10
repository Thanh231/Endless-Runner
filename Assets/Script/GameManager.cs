using System.Collections;
using UnityEngine;

public enum GameState
{
    Init,
    StartGame,
    
    PauseGame,
}

public class GameManager : Singleton<GameManager>
{
    private GameState currentState;
    private void OnEnable()
    {
        currentState = GameState.Init;
        Time.timeScale = 0f;
        EventManager.OnPlayerDied += GameOver;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerDied -= GameOver;
    }

    void GameOver()
    {
        currentState = GameState.PauseGame;
        UIManager.Ins.ShowGameOver();
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        StartCoroutine(DelayStartGame());
    }

    private IEnumerator DelayStartGame()
    {
        yield return new WaitForSeconds(1f);
        currentState = GameState.StartGame;
        EventManager.OnStartGame?.Invoke();
    }
}
