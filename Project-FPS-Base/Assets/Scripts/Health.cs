using System;
using NUnit.Framework;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int maxHealth = 10;

    private int _currentHealth = 0;
    public Action<bool> IsDie;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damage;
        }
        else
        {
            IsDie?.Invoke(true);
        }
    }
}
