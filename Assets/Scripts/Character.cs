using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : Target
{
    [SerializeField] private int _maximalHealth;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _itemPickupDistance;
    [SerializeField] private float _enemyDetectDistance;
    [SerializeField] private float _itemDetectDistance;

    private Target _target;
    private int _currentHealth;

    public int EnemyLayerMask { get; protected set; }
    public int MedicineLayerMask { get; protected set; }

    public int MaximalHealth => _maximalHealth;
    public int CurrentHealth => _currentHealth;
    public float AttackDistance => _attackDistance;
    public float StopDistance => _itemPickupDistance;
    public float EnemyDetectDistance => _enemyDetectDistance;
    public float ItemDetectDistance => _itemDetectDistance;
    
    public Target Target => _target;

    private void Start()
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

    public void SetTarget(Target target)
    { 
        _target = target;
    }
}
