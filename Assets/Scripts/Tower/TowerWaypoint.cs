using UnityEngine;

public class TowerWaypoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable = true;
    [SerializeField] GameObject[] towerPrefabs;
    [SerializeField] GameObject towerPrefab;

    bool isTowerSelected = false;

    void SetUpSelectedTower()
    {
        foreach (GameObject prefab in towerPrefabs)
        {
            Tower tower = prefab.GetComponentInChildren<Tower>();
            if (TowersUI.Instance.SelectedTower == tower.GetTowerType())
            {
                towerPrefab = prefab;
                isTowerSelected = true;
                return;
            }
        }
    }

    void OnMouseDown()
    {
        SetUpSelectedTower();
        if (towerPrefab == null) { return; }

        Tower tower = towerPrefab.GetComponentInChildren<Tower>();

        if (isTowerSelected && isPlaceable && tower.TowerData.towerCost < Bank.Instance.GetCurrentBalance()) 
        {
            Bank.Instance.Withdraw(tower.TowerData.towerCost);
            tower.CreateTower(towerPrefab, transform.position + new Vector3(0, towerPrefab.transform.position.y - 0.5f, 0));
            isPlaceable = false;
            isTowerSelected = false;
        }
        else if (tower.TowerData.towerCost >= Bank.Instance.GetCurrentBalance())
        {
            GameManager.Instance.DisplayLowBalanceText(true);
            Invoke("DisableLowBalanceText", 2f);
        }
    }

    void DisableLowBalanceText()
    {
        GameManager.Instance.DisplayLowBalanceText(false);
    }
}
