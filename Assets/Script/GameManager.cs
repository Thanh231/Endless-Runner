using System;
using System.Collections;
using UnityEngine;

public enum GameState
{
    Init,
    StartGame,

    PauseGame,
    GameOver
}

public class GameManager : Singleton<GameManager>
{
    private GameState currentState;
    private void OnEnable()
    {
        currentState = GameState.Init;
        Time.timeScale = 0f;
        EventManager.OnPlayerDied += PlayerDied;
        EventManager.OnGameOver += ResetGame;
    }

    private void ResetGame()
    {
        currentState = GameState.GameOver;
        UIManager.Ins.ShowGameOver();
    }

    private void OnDisable()
    {
        EventManager.OnPlayerDied -= PlayerDied;
    }

    void PlayerDied()
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
        yield return new WaitForSeconds(0.3f);
        currentState = GameState.StartGame;
        EventManager.OnStartGame?.Invoke();
    }
}
