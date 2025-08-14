using UnityEngine;

public class Player : MonoBehaviour
{
    public float speedTranslate = 5f;
    public Vector3 defaultPlayerPosition = Vector3.zero;

    public bool isPlaying = false;

    private void OnEnable()
    {
        EventManager.OnStartGame += PlayGame;
        EventManager.OnStopGame += StopGame;
        EventManager.OnGameOver += EndGame;
    }
    void OnDisable()
    {
        EventManager.OnStartGame -= PlayGame;
        EventManager.OnStopGame -= StopGame;
        EventManager.OnGameOver -= EndGame;
    }

    private void EndGame()
    {
        transform.position = defaultPlayerPosition;
        isPlaying = false;
    }

    void Start()
    {
        defaultPlayerPosition = transform.position;
    }

    private void PlayGame()
    {
        isPlaying = true;
    }

    private void StopGame()
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
