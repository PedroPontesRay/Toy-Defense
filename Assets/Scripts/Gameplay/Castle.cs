using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    #region Variables
    [Header("Valores de vida")]
    [SerializeField] private int maxLife;
    [NonSerialized]  public float currentLife;
    [SerializeField] private Image _foreground;
    private Interface_Manager _manager;

    #endregion


    #region Methods

    private void Start()
    {
        _manager = FindAnyObjectByType<Interface_Manager>();
        currentLife = maxLife;
    }


    public void Damage(int damageByTheEnemy)
    { 
        currentLife -= damageByTheEnemy;

        UpdateLife(maxLife,currentLife);

        if(currentLife <= 0)
        {

        }
    }

    private void UpdateLife(int maxHealth,float currentHealth)
    {
        _foreground.fillAmount = currentHealth / maxHealth;
    }

    #endregion
}
