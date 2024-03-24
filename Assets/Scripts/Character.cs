using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : Target
{
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _itemPickupDistance;
    [SerializeField] private float _enemyDetectDistance;
    [SerializeField] private float _itemDetectDistance;

    private Target _target;

    public int EnemyLayerMask { get; protected set; }
    public int MedicineLayerMask { get; protected set; }

    public float AttackDistance => _attackDistance;
    public float StopDistance => _itemPickupDistance;
    public float EnemyDetectDistance => _enemyDetectDistance;
    public float ItemDetectDistance => _itemDetectDistance;
    
    public Target Target => _target;


    public void SetTarget(Target target)
    { 
        _target = target;
    }
}
