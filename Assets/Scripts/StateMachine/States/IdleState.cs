using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private Animator _animator;
    private Character _character;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _character = GetComponent<Character>();
    }

    private void OnEnable()
    {
        _animator.SetBool("IsMoving_b", false);
        _animator.SetBool("IsAttacking_b", false);
    }
}
