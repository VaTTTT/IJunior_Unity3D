using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterMoverTest : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _isStatic;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.SetFloat("Speed_f", _speed);
        _animator.SetBool("Static_b", _isStatic);
    }
}