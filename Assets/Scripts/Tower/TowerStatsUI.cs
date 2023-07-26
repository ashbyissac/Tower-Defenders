using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerStatsUI : MonoBehaviour
{
    [SerializeField] GameObject towerStatsUI;
    
    [SerializeField] TextMeshProUGUI towerLevelText; 
    [SerializeField] TextMeshProUGUI towerRangeText; 
    [SerializeField] TextMeshProUGUI shootDelayText; 
    [SerializeField] TextMeshProUGUI hitDamageText;
    [SerializeField] TextMeshProUGUI upgradeCostText;

    int towerLevel;
    float towerRange;
    float shootDelay;
    float hitDamage;
    float upgradeCost;

    public static TowerStatsUI Instance;

    void Awake()
    {
        Instance = this;
    }

    public void DisplayTowerStats()
    {
        towerLevelText.text = $"TOWER LEVEL {towerLevel}";
        towerRangeText.text = $"TOWER RANGE : {towerRange}";
        shootDelayText.text = $"SHOOT DELAY : {shootDelay}";
        hitDamageText.text = $"HIT DAMAGE : {hitDamage}";
        upgradeCostText.text = $"UPGRADE COST : {upgradeCost}";
    }

    public void EnableTowerStatsUI()
    {
        towerStatsUI.SetActive(true);
    }

    public void InitializeCurrentTowerLevel(int level)
    {
        towerLevel = level;
    }

    public void InitializeCurrentTowerRange(float range) 
    {
        towerRange = range;
    }

    public void InitializeCurrentShootDelay(float delay) 
    {
        shootDelay = delay;
    }

    public void InitializeCurrentHitDamage(float damage) 
    {
        hitDamage = damage;
    }

    public void InitializeCurrentUpgradeCost(int cost)
    {
        upgradeCost = cost;
    }
}
