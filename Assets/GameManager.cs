using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private bool hasGameStarted = false;

    private bool isGameOver = false;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                Debug.LogError("Game Manager is Null!!!");


            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.anyKeyDown)
        {
            hasGameStarted = true;
        }
    }

    public void GameOver(bool flag)
    {
        IsGameOver = flag;
    }

    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }
    public bool HasGameStarted { get => hasGameStarted; set => hasGameStarted = value; }
}
