using UnityEngine;

public enum GameState
{
    Init,
    Playing,
    GameOver,
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
        currentState = GameState.GameOver;
        UIManager.Ins.ShowGameOver();
    }

    public void PlayGame()
    {
        currentState = GameState.Playing;
        EventManager.OnStartGame?.Invoke();
        Time.timeScale = 1f;
    }
}
