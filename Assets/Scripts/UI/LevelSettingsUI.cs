using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSettingsUI : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject pauseSystemUI;
    [SerializeField] TextMeshProUGUI currentBalanceText;
    [SerializeField] TextMeshProUGUI lowBalanceText;
    [SerializeField] TextMeshProUGUI waveSignalText;

    void Awake()
    {
        GameManager.Instance.InitializeUIComponents(gameOverUI, currentBalanceText, lowBalanceText, waveSignalText);
    }

    void Start()
    {
        if (!GameManager.Instance.IsRestarting) { UpgradeCoinsPerLevel(); }
        Bank.Instance.CurrentSceneBalance = Bank.Instance.GetCurrentBalance();
        GameManager.Instance.DisplayCurrentBalance(Bank.Instance.CurrentSceneBalance);
    }

    void UpgradeCoinsPerLevel()
    {
        int currentBalance = Bank.Instance.GetCurrentBalance();
        string activeSceneName = SceneManager.GetActiveScene().name;

        if (activeSceneName == "Summer1 Redesigned") { return; }
        else if (activeSceneName == "Winter Redesigned")
        {
            currentBalance += 70;
        }
        else if (activeSceneName == "Summer2 Redesigned")
        {
            currentBalance += 80;
        }

        Bank.Instance.SetCurrentBalance(currentBalance);
        GameManager.Instance.DisplayCurrentBalance(currentBalance);
    }

    void Update()
    {
        /*
         * Debug Keys
         * 
         * 
         
        if (Input.GetKeyDown(KeyCode.D)) // TODO :: Remove
        {
            Bank.Instance.Deposit(10);
        }

        if (Input.GetKey(KeyCode.F)) // TODO :: Remove
        {
            Bank.Instance.Withdraw(1);
        }

        *
        *
       */

        if (pauseSystemUI == null) { return; }
        PauseGame();
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseSystemUI.gameObject.SetActive(!pauseSystemUI.activeInHierarchy);
            if (pauseSystemUI.activeInHierarchy)
            {
                //Invoke("SetTimeScale", 0.8f);
                GameManager.Instance.SetTimeScale(0f);
            }
            else
            {
                GameManager.Instance.SetTimeScale(1f);
            }
        }
    }

    public void RestartLevel()
    {
        GameManager.Instance.SetTimeScale(1f);
        GameManager.Instance.IsRestarting = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


        Debug.Log("LevelSettingsUI Current Scene balance : " + Bank.Instance.CurrentSceneBalance);
        Bank.Instance.SetCurrentBalance(Bank.Instance.CurrentSceneBalance); // startingBalance
    }

    public void ResumeGame()
    {
        GameManager.Instance.SetTimeScale(1f);
        pauseSystemUI.gameObject.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
        GameManager.Instance.SetTimeScale(1f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
