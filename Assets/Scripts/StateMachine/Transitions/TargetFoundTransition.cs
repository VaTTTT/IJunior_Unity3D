using System.Linq;
using UnityEngine;

public class TargetFoundTransition : Transition
{
    private Character _character;
    private Collider[] _targetsColliders;
    private Target _closestTarget;
    private bool _isTargetFound;

    private void Start()
    {
        _character = GetComponent<Character>();
    }

    protected override void ChangeState()
    {
        _closestTarget = GetClosestTarget(_character.EnemyLayerMask, _character.EnemyDetectDistance);

        if (_closestTarget != null)
        {
            _isTargetFound = true;
        }
        else if (_character.TryGetComponent<Health>(out Health health) && health.CurrentValue < health.MaximalValue)
        {
            _closestTarget = GetClosestTarget(_character.MedicineLayerMask, _character.ItemDetectDistance);

            if (_closestTarget != null)
            {
                _isTargetFound = true;
            }
        }
        else if (_character.MainTarget != null)
        { 
            _closestTarget = _character.MainTarget;
            _isTargetFound = true;
        }
        else
        {
            _isTargetFound = false;
        }

        if (_isTargetFound && _closestTarget != _character.Target)
        {
            _character.SetTarget(_closestTarget);
            NeedTransit = true;
        }
        else if(!_isTargetFound)
        {
            _character.SetTarget(null);
        }
    }

    private Target GetClosestTarget(LayerMask searchLayer, float distance)
    {
        Target closestTarget = null;

        _targetsColliders = Physics.OverlapSphere(transform.position, distance, searchLayer);

        if (_targetsColliders.Length > 0)
        {
            closestTarget = _targetsColliders.OrderBy(target => Vector3.Distance(target.transform.position, transform.position)).FirstOrDefault().GetComponent<Target>();
        }

        return closestTarget;
    }
}
