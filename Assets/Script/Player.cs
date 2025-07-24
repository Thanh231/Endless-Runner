using UnityEngine;

public class Player : MonoBehaviour
{
    public float speedTranslate = 5f;
    
    private bool isPlaying = false;

    private void OnEnable()
    {
        EventManager.OnStartGame += OnPlayGame;
        EventManager.OnPlayerDied += OnEndGame;
    }
    void OnDisable()
    {
        EventManager.OnStartGame -= OnPlayGame;
        EventManager.OnPlayerDied -= OnEndGame;
    }

    private void OnPlayGame()
    {
        isPlaying = true;
    }

    private void OnEndGame()
    {
        isPlaying = false;
    }
    void Update()
    {
        if (isPlaying)
        {
            transform.Translate(transform.forward * speedTranslate * Time.deltaTime);
        }
    }
}
