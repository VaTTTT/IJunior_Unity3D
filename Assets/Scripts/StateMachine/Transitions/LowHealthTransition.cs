using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LowHealthTransition : Transition
{
    private Character _character;

    private void Start()
    {
        _character = GetComponent<Character>();
    }

    protected override void ChangeState()
    {
        if (_character.CurrentHealth < _character.InitialHealth && _character.Target == null)
        {
            NeedTransit = true;
        }
    }
}
