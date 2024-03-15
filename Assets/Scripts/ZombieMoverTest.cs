using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ZombieMoverTest : MonoBehaviour
{
    [SerializeField] private bool _isWalking;
    [SerializeField] private bool _isCrawling;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.SetBool("IsCrawling", _isCrawling);
        _animator.SetBool("IsWalking", _isWalking);
    }
}
