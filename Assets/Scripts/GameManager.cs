using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private bool hasGameStarted = false;

    private bool isGameOver = false;

    [SerializeField]
    private Slider powerUpSlider;

    private float powerUpScore;

    private float powerUpVelocity;

    private int moneyScore = 0;

    public TextMeshProUGUI scoreText;

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

        if (hasGameStarted)
        {
            UpdatePowerUpScore();
            DecreaseSlider();
            UpdateScore();
        }
    }

    public void GameOver(bool flag)
    {
        IsGameOver = flag;
        DeactivateChasingPeople();
    }

    private void DeactivateChasingPeople()
    {
        GameObject[] peoples = GameObject.FindGameObjectsWithTag("People");

        foreach (GameObject people in peoples)
        {
            people.GetComponent<People>().IsActive = false;
        }
    }

    private void UpdatePowerUpScore()
    {
        if (powerUpSlider.value >= powerUpSlider.maxValue)
        {
            StartCoroutine(FindObjectOfType<Thief>().SpeedUpExplosionBoost());
            powerUpScore = 0;
        }

        float currentScore = Mathf.SmoothDamp(powerUpSlider.value, powerUpScore, ref powerUpVelocity, 10 * Time.deltaTime);
        powerUpSlider.value = currentScore;
    }

    private void DecreaseSlider()
    {
        powerUpScore -= 2.5f * Time.deltaTime;
    }

    private void UpdateScore()
    {
        scoreText.text = moneyScore.ToString();
    }

    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }
    public bool HasGameStarted { get => hasGameStarted; set => hasGameStarted = value; }
    public float PowerUpScore { get => powerUpScore; set => powerUpScore = value; }
    public int MoneyScore { get => moneyScore; set => moneyScore = value; }
}
