using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speedTranslate = 5f;
    public Vector3 defaultPlayerPosition = Vector3.zero;
    
    public bool isPlaying = false;

    private void OnEnable()
    {
        EventManager.OnStartGame += OnPlayGame;
        EventManager.OnPlayerDied += OnPlayerDied;
        EventManager.OnGameOver += OnEndGame;
    }
    void OnDisable()
    {
        EventManager.OnStartGame -= OnPlayGame;
        EventManager.OnPlayerDied -= OnPlayerDied;
        EventManager.OnGameOver -= OnEndGame;
    }

    private void OnEndGame()
    {
        transform.position = defaultPlayerPosition;
        isPlaying = false;
    }

    void Start()
    {
        defaultPlayerPosition = transform.position;
    }

    private void OnPlayGame()
    {
        isPlaying = true;
    }

    private void OnPlayerDied()
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
