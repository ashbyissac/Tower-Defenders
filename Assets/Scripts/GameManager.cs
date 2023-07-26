using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 3f;

    GameObject gameOverUI; 
    TextMeshProUGUI currentBalanceText;
    TextMeshProUGUI lowBalanceText;
    TextMeshProUGUI waveSignalText;

    public static GameManager Instance = null;

    bool isRestarting;
    public bool IsRestarting { get { return isRestarting; } set { isRestarting = value; } }

    int currentLevel;
    float loadTimeElapsed = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    public void InitializeUIComponents(GameObject gameOverUI, TextMeshProUGUI currentBalanceText, 
                                        TextMeshProUGUI lowBalanceText, TextMeshProUGUI waveSignalText)
    {
        this.gameOverUI = gameOverUI;
        this.currentBalanceText = currentBalanceText;
        this.lowBalanceText = lowBalanceText;
        this.waveSignalText = waveSignalText;
    }

    void Update()
    {
        if (EnemySpawner.Instance.IsCurrentLevelOver && Bank.Instance.GetCurrentBalance() > 0) 
        {
            loadTimeElapsed += Time.deltaTime;
            if (loadTimeElapsed > levelLoadDelay)
            {
                EnemySpawner.Instance.IsCurrentLevelOver = false;
                loadTimeElapsed = 0f;
                LoadNextScene(); 
            }
        }
    }

    public void StartGame()
    {
        Invoke("LoadNextScene", 2f);
    }

    void LoadNextScene()
    {
        isRestarting = false;
        Debug.Log("Current Level : " + currentLevel);
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (currentLevel < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentLevel + 1);
        }
    }

    public void GameOverUI(bool state)
    {
        gameOverUI.SetActive(state);
        SetTimeScale(0f);
    }

    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    public void DisplayCurrentBalance(int currentBalance)
    {
        currentBalanceText.text = currentBalance.ToString();
    }

    public void DisplayWaveSignalText(bool state)
    {
        waveSignalText.gameObject.SetActive(state);
    }

    public void DisplayLowBalanceText(bool state)
    {
        lowBalanceText.gameObject.SetActive(state);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
