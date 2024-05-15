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
    private Target _mainTarget;

    public int EnemyLayerMask { get; protected set; }
    public int MedicineLayerMask { get; protected set; }

    public float AttackDistance => _attackDistance;
    public float StopDistance => _itemPickupDistance;
    public float EnemyDetectDistance => _enemyDetectDistance;
    public float ItemDetectDistance => _itemDetectDistance;
    public Target MainTarget => _mainTarget;
    
    public Target Target => _target;

    public override void Select()
    {
        _isSelected = true;

        if (_frame != null)
        {
            _frame.SetActive(true);
        }
    }

    public override void Deselect()
    {
        _isSelected = false;

        if (_frame != null)
        {
            _frame.SetActive(false);
        }
    }

    public void SetTarget(Target target)
    { 
        _target = target;
    }

    public void SetMainTarget(Target target)
    { 
        _mainTarget = target;
    }
}