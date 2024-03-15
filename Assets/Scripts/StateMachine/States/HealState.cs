using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealState : State
{
    [SerializeField] private float _speed;
    private Collider[] _targetsColliders;
    private Item _closestTarget;
    private Animator _animator;
    private Character _character;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _character = GetComponent<Character>();
    }

    private void OnEnable()
    {
        _animator.SetBool("IsMoving_b", true);
    }

    private void OnDisable()
    {
        _animator.SetBool("IsMoving_b", false);
    }

    private void Update()
    {
        _targetsColliders = Physics.OverlapSphere(transform.position, _character.EnemyDetectDistance, _character.MedicineLayerMask);

        if (_targetsColliders.Length > 0)
        {
            _closestTarget = _targetsColliders.OrderBy(target => Vector3.Distance(target.transform.position, transform.position)).FirstOrDefault().GetComponent<Item>();

            transform.LookAt(_closestTarget.transform.position);
            transform.Translate(_speed * Time.deltaTime * Vector3.forward);
        }
    }
}
