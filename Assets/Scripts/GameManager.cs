using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using System.Collections;

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

    public Score score;

    public GameObject moneyScoreCanvas;
    public GameObject levelProgressCanvas;


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
        UpdateScore();
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

    public void ToggleOffUI()
    {
        powerUpSlider.gameObject.SetActive(false);
        moneyScoreCanvas.SetActive(false);
        levelProgressCanvas.SetActive(false);
    }

    public void EndGame()
    {
        IsGameOver = true;
        Invoke(nameof(Restart), restartDelay);
        StartCoroutine(RestartScore());
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
        if (SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings)
        {
            Restart();
        }

        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    private void OnApplicationQuit()
    {
        score.Value = 0;
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
        scoreText.text = score.Value.ToString();
    }

    IEnumerator RestartScore()
    {
        yield return new WaitForSeconds(2.5f);
        score.Value = 0;
    }

    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }
    public bool HasGameStarted { get => hasGameStarted; set => hasGameStarted = value; }
    public float PowerUpScore { get => powerUpScore; set => powerUpScore = value; }
    public int MoneyScore { get => moneyScore; set => moneyScore = value; }
    public bool HasPassedFinishLine { get => hasPassedFinishLine; set => hasPassedFinishLine = value; }
    public bool IsLevelFinished { get => isLevelFinished; set => isLevelFinished = value; }
}
