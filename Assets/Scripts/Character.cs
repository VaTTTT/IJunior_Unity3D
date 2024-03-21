using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : Target
{
    [SerializeField] private int _initialHealth;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _itemPickupDistance;
    [SerializeField] private float _enemyDetectDistance;
    [SerializeField] private float _itemDetectDistance;

    private Target _target;
    private int _currentHealth;

    public int EnemyLayerMask { get; protected set; }
    public int MedicineLayerMask { get; protected set; }

    public int InitialHealth => _initialHealth;
    public int CurrentHealth => _currentHealth;
    public float AttackDistance => _attackDistance;
    public float StopDistance => _itemPickupDistance;
    public float EnemyDetectDistance => _enemyDetectDistance;
    public float ItemDetectDistance => _itemDetectDistance;
    
    public Target Target => _target;

    private void Start()
    {
        _currentHealth = _initialHealth;
    }

    public void ApplyDamage(int damage)
    {
        if (_currentHealth - damage > 0)
        {
            _currentHealth -= damage;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ApplyHealing(int amount)
    {
        if (_currentHealth + amount <= _initialHealth)
        {
            _currentHealth += amount;
        }
        else
        {
            _currentHealth = _initialHealth;
        }
    }

    public void SetTarget(Target target)
    { 
        _target = target;
    }
}
