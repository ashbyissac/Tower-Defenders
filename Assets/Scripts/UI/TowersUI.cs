using UnityEngine;

public class TowersUI : MonoBehaviour
{
    [SerializeField] TowerType selectedTower;
    public TowerType SelectedTower { get { return selectedTower; } }

    public static TowersUI Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
    }

    public void SelectBulletTower()
    {
        selectedTower = TowerType.BulletTower;
    }

    public void SelectArrowTower()
    {
        selectedTower = TowerType.ArrowTower;
    }

    public void SelectCanonTower()
    {
        selectedTower = TowerType.CanonTower;
    }
}
