using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MedicineFoundTransition : Transition
{
    private Character _character;
    private LayerMask _targetsLayerMask;
    private Collider[] _targetsColliders;
    private Character _closestTarget;

    private void Start()
    {
        _character = GetComponent<Character>();
        _targetsLayerMask = _character.EnemyLayerMask;
    }

    protected override void ChangeState()
    {
        _targetsColliders = Physics.OverlapSphere(transform.position, _character.EnemyDetectDistance, _targetsLayerMask);

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
    }
}
