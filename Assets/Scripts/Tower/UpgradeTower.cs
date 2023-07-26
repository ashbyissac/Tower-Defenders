using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTower : MonoBehaviour
{
    [SerializeField] Tower currentlySelectedTower;
    public Tower CurrentlySelectedTower { get { return currentlySelectedTower; } set { currentlySelectedTower = value; } }

    [SerializeField] Button button;

    public static UpgradeTower Instance;

    void Awake()
    {
        Instance = this;

        if (button == null) button = GetComponent<Button>();
        if (button != null) button.onClick.AddListener(UpgradeTowerOnClick);
    }

    public void UpgradeTowerOnClick()
    {
        if (currentlySelectedTower.TowerData.currentTowerLevel < currentlySelectedTower.TowerData.maxTowerLevel)
        {
            currentlySelectedTower.UpgradeTowerPower();
            currentlySelectedTower.GetComponent<TowerTargetLocator>().SetAndDisplayStats();
        }
    }
}
