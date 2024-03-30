using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maximalHealth;
    
    private int _currentHealthValue;
    private int _previousHealthValue;

    public event UnityAction<int,int> HealthChanged;

    public int MaximalHealthValue => _maximalHealth;
    public int CurrentHealthValue => _currentHealthValue;

    private void Start()
    {
        _currentHealthValue = _maximalHealth;
        _previousHealthValue = _currentHealthValue;
    }

    public void ApplyDamage(int damage)
    {
        _previousHealthValue = _currentHealthValue;
        _currentHealthValue = Mathf.Clamp(_currentHealthValue - damage, 0, _maximalHealth);

        HealthChanged?.Invoke(_previousHealthValue, _currentHealthValue);

        if (_currentHealthValue == 0)
        {
            Destroy(gameObject);
        }
    }

    public void ApplyHealing(int amount)
    {
        _currentHealthValue = Mathf.Clamp(_currentHealthValue + amount, 0, _maximalHealth);

        HealthChanged?.Invoke(_previousHealthValue, _currentHealthValue);
    }
}
