using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TargetAttacker : MonoBehaviour
{
    [SerializeField] private float _attackSpeed;
    [SerializeField] private int _damage;
    
    private Animator _animator;
    private float _attackTimeCounter;
    private float _animationLength;
    private CharacterMover _characterMover;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.SetBool("IsAttacking_b", true);
    }

    private void OnDisable()
    {
        _animator.SetBool("IsAttacking_b", false);
    }

    private void Start()
    {
        _animationLength = 1.167f;
        _characterMover = GetComponent<CharacterMover>();
    }

    private void Update()
    { 
        _attackTimeCounter += Time.deltaTime * _attackSpeed;

        if (_attackTimeCounter >= _animationLength) 
        {
            _characterMover.GetReachedCharacter().ApplyDamage(_damage);
            _attackTimeCounter = 0;
        }
    }
}
