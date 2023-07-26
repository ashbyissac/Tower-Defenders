using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] int maxHealth;
    
    Slider slider;
    EnemyHealth enemyHealth;

    void OnEnable() // todo :: in awake
    {
        slider = GetComponent<Slider>();
        enemyHealth = GetComponentInParent<EnemyHealth>();

        maxHealth = enemyHealth.GetMaxEnemyHealth();
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void UpdateEnemyHealthBar(int healthRem)
    {
        slider.value = healthRem;
    }
}
