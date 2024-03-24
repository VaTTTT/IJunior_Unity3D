using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maximalHealth;
    
    private int _currentHealth;

    public int MaximalHealth => _maximalHealth;
    public int CurrentHealth => _currentHealth;

    void Start()
    {
        _currentHealth = _maximalHealth;
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maximalHealth);

        if (_currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }

    public void ApplyHealing(int amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maximalHealth);
    }
}
