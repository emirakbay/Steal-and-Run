using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private bool hasGameStarted = false;

    private bool isGameOver = false;

    [SerializeField]
    private Slider powerUpSlider;

    private float powerUpScore;

    private float powerUpVelocity;

    private float powerLow;

    private Thief thief;

    private bool discharge = false;

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

    private void Start()
    {
        thief = FindObjectOfType<Thief>();
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

        if (hasGameStarted)
        {
            UpdatePowerUpScore();
            DecreaseSlider();
        }
    }

    public void GameOver(bool flag)
    {
        IsGameOver = flag;
    }

    private void UpdatePowerUpScore()
    {
        if (powerUpSlider.value >= powerUpSlider.maxValue)
        {
            powerUpScore = 0;
        }

        else if (powerUpSlider.value <= 0)
        {
            print("0 point");
        }

        print("update score");
        float currentScore = Mathf.SmoothDamp(powerUpSlider.value, powerUpScore, ref powerUpVelocity, 50 * Time.deltaTime);
        powerUpSlider.value = currentScore;
    }

    private void DecreaseSlider()
    {
        print("decrase score");
        powerUpScore -= 2.5f * Time.deltaTime;
    }

    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }
    public bool HasGameStarted { get => hasGameStarted; set => hasGameStarted = value; }
    public float PowerUpScore { get => powerUpScore; set => powerUpScore = value; }
}
