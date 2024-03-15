using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetReachedTransition : Transition
{
    private Character _character;

    private void Start()
    {
        _character = GetComponent<Character>();
    }

    protected override void ChangeState()
    {
        if (_character.Target != null) 
        {
            if (Vector3.Distance(_character.Target.transform.position, transform.position) <= _character.AttackDistance)
            {
                NeedTransit = true;
            }
        }
    }
}
