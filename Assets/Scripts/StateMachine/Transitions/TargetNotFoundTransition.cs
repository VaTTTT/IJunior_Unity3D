using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetNotFoundTransition : Transition
{
    private Character _character;

    private void Start()
    {
        _character = GetComponent<Character>();
    }

    protected override void ChangeState()
    {
        if (_character.Target == null)
        {
            NeedTransit = true;
        }
    }
}
