using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private bool hasGameStarted = false;

    private bool isGameOver = false;

    private bool hasPassedFinishLine = false;

    private bool isLevelFinished = false;

    [SerializeField]
    private Slider powerUpSlider;

    private float powerUpScore;

    private float powerUpVelocity;

    private float restartDelay = 2.5f;

    private float loadNextLevelDelay = 3.5f;

    private int moneyScore = 0;

    public TextMeshProUGUI scoreText;

    public CinemachineVirtualCamera levelEndCam;

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

        PowerUpScore = powerUpSlider.value;
    }

    private void Update()
    {
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

    public void EndGame()
    {
        IsGameOver = true;
        print("game over");
        Invoke(nameof(Restart), restartDelay);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver(bool flag)
    {
        IsGameOver = flag;
        DeactivateChasingPeople();
        EndGame();
    }

    public void LevelComplete()
    {
        IsLevelFinished = true;
        Invoke(nameof(LoadNextLevel), loadNextLevelDelay);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LevelEndCamera()
    {
        levelEndCam.Priority += 1;
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
        if (powerUpSlider.value >= powerUpSlider.maxValue && !HasPassedFinishLine)
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
    public bool HasPassedFinishLine { get => hasPassedFinishLine; set => hasPassedFinishLine = value; }
    public bool IsLevelFinished { get => isLevelFinished; set => isLevelFinished = value; }
}
