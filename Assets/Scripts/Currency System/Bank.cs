using UnityEngine;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 100;
    public int StartingBalance { get { return startingBalance; } }

    public static Bank Instance;

    GameManager gameManager;
    int currentBalance;
    int currentSceneBalance;
    public int CurrentSceneBalance 
    { get { return currentSceneBalance; } set { currentSceneBalance = value; } }

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

        currentBalance = startingBalance;
    }

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    /*
     * Increases the number of coins when
     *  -> enemy is killed by tower
     */
    public void Deposit(int depositAmount)
    {
        currentBalance += depositAmount;
        gameManager.DisplayCurrentBalance(currentBalance);
    }

    /*
     * Reduces the number of coins when
     *  -> enemy reaches endpoint
     *  -> tower's upgraded
     *  -> tower's instantiated
     */
    public void Withdraw(int withdrawAmount)
    {
        currentBalance -= withdrawAmount;
        if (currentBalance < 1)
        {
            currentBalance = 0;
            gameManager.DisplayCurrentBalance(currentBalance);
            gameManager.GameOverUI(true);
            return;
        }

        gameManager.DisplayCurrentBalance(currentBalance);
    }

    public int GetCurrentBalance()
    {
        return currentBalance;
    }
    
    public void SetCurrentBalance(int currentBalance)
    {
        this.currentBalance = currentBalance;
    }
}
