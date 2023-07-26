using System;
using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{ 
    [SerializeField] TowerType towerType;

    [Serializable]
    public class TowerDatas
    {
        public int currentTowerLevel;
        public int maxTowerLevel;
        public int towerCost;
        public float towerRangeFactor;
        public float shootDelayFactor;
        public int hitDamageFactor;
        public int upgradeCost;
        public int upgradeCostFactor;
    }

    static Tower previouslySelectedTower;
    [SerializeField] TowerDatas towerData;
    public TowerDatas TowerData { get { return towerData; } }

    public void CreateTower(GameObject towerPrefab, Vector3 position)
    {
        Instantiate(towerPrefab, position, Quaternion.identity);
    }

    public void ShowTowerStats()
    {
        if (previouslySelectedTower != null)
        {
            Light light = previouslySelectedTower.GetComponentInChildren<Light>();
            light.enabled = false;
        }
        
        GetComponentInChildren<Light>().enabled = true;
        TowerStatsUI.Instance.InitializeCurrentUpgradeCost(towerData.upgradeCost);
        TowerStatsUI.Instance.InitializeCurrentTowerLevel(towerData.currentTowerLevel);
        GetComponent<TowerTargetLocator>().SetAndDisplayStats();
        TowerStatsUI.Instance.EnableTowerStatsUI();

        UpgradeTower.Instance.CurrentlySelectedTower = this;
        previouslySelectedTower = this;
    }

    public void UpgradeTowerPower()
    {
        TowerTargetLocator towerTargetLocator = GetComponent<TowerTargetLocator>();
        Bank bank = Bank.Instance;
        towerTargetLocator.SetAndDisplayStats();

        if (towerData.upgradeCost < bank.GetCurrentBalance())
        {
            if (towerTargetLocator.towerTag == "Arrow")
            {
                bank.Withdraw(towerData.upgradeCost);
                UpgradeTowerStats(towerTargetLocator);
            }
            else if (towerTargetLocator.towerTag == "Bullet")
            {
                bank.Withdraw(towerData.upgradeCost);
                UpgradeTowerStats(towerTargetLocator);
            }
            else if (towerTargetLocator.towerTag == "Canon")
            {
                bank.Withdraw(towerData.upgradeCost);
                UpgradeTowerStats(towerTargetLocator);
            }
        }
        else
        {
            GameManager.Instance.DisplayLowBalanceText(true);
            Invoke("DisableLowBalanceText", 2f);
        }
        
        SetUpgradeStats();
    }

    void DisableLowBalanceText()
    {
        GameManager.Instance.DisplayLowBalanceText(false);
    }

    void UpgradeTowerStats(TowerTargetLocator towerTargetLocator)
    {
        towerTargetLocator.UpgradeTowerRange(towerData.towerRangeFactor);
        towerTargetLocator.UpgradeShootDelay(towerData.shootDelayFactor);
        towerTargetLocator.UpgradeHitDamage(towerData.hitDamageFactor);
        towerData.upgradeCost += towerData.upgradeCostFactor;
        towerData.currentTowerLevel++;
    }

    void SetUpgradeStats()
    {
        TowerStatsUI.Instance.InitializeCurrentUpgradeCost(towerData.upgradeCost);
        TowerStatsUI.Instance.InitializeCurrentTowerLevel(towerData.currentTowerLevel);
        GetComponent<TowerTargetLocator>().SetAndDisplayStats();
        TowerStatsUI.Instance.EnableTowerStatsUI();
    }

    public TowerType GetTowerType()
    {
        return towerType;
    }
}
