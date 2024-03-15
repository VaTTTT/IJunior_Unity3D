using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetFoundTransition : Transition
{
    private Character _character;
    //private LayerMask _targetsLayerMask;
    private Collider[] _targetsColliders;
    private Target _closestTarget;

    private int _currentPatrolPointIndex;
    private int _patrolPointsNumber;
    [SerializeField] private float _speed;
    [SerializeField] private float _stopDistance;

    [SerializeField] private PatrolPoint[] _patrolPoints;

    private void Start()
    {
        _character = GetComponent<Character>();
        //_targetsLayerMask = _character.EnemyLayerMask;
        _patrolPointsNumber = _patrolPoints.Length;
        _currentPatrolPointIndex = 0;
    }

    protected override void ChangeState()
    {
        _closestTarget = GetClosestTarget(_character.EnemyLayerMask);

        if (!_closestTarget)
        {
            NeedTransit = true;
        }
        else if (_character.CurrentHealth < _character.InitialHealth)
        {
            _closestTarget = GetClosestTarget(_character.MedicineLayerMask);
            NeedTransit = true;
        }
        else if (_character.PatrolPoints.Length > 0)
        {
            _currentPatrolPointIndex = ++_currentPatrolPointIndex % _patrolPointsNumber;
            NeedTransit = true;
        }
    }

    private Target GetClosestTarget(LayerMask searchLayer)
    {
        Target target = null;

        _targetsColliders = Physics.OverlapSphere(transform.position, _character.EnemyDetectDistance, searchLayer);

        if (_targetsColliders.Length > 0)
        {
            _closestTarget = _targetsColliders.OrderBy(target => Vector3.Distance(target.transform.position, transform.position)).FirstOrDefault().GetComponent<Character>();

            if (_closestTarget != _character.Target)
            {
                _character.SetTarget(_closestTarget);

                NeedTransit = true;
            }
        }
        else
        {
            _character.SetTarget(null);
        }

        return target;
    }
}
