using System;
using System.ComponentModel;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _itemPickUpDistance;
    [SerializeField] private Target[] _patrolPoints;
    
    private int _currentPatrolPointIndex;
    private int _patrolPointsNumber;
    private Target _currentTarget;
    private Animator _animator;
    private Vector3 _targetPosition;
    private TargetAttacker _targetAttacker;

    public float AttackDistance => _attackDistance;
    public Target Target => _currentTarget;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _targetAttacker = GetComponent<TargetAttacker>();
        _patrolPointsNumber = _patrolPoints.Length;
        _currentPatrolPointIndex = 0;
    }

    private void Update()
    {
        if (_currentTarget != null)
        {
            _targetPosition = _currentTarget.transform.position;

            if (_currentTarget.TryGetComponent<Character>(out _) && Vector3.Distance(transform.position, _targetPosition) > _attackDistance)
            {
                _animator.SetBool("IsMoving_b", true);
                _targetAttacker.enabled = false;
                transform.LookAt(_targetPosition);
                transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            }
            else if (!_currentTarget.TryGetComponent<Character>(out _))
            {
                if (Vector3.Distance(transform.position, _targetPosition) > _itemPickUpDistance)
                {
                    _animator.SetBool("IsMoving_b", true);
                    _targetAttacker.enabled = false;
                    transform.LookAt(_targetPosition);
                    transform.Translate(Vector3.forward * _speed * Time.deltaTime);
                }
                else if (_patrolPointsNumber > 0)
                {
                    _currentPatrolPointIndex = ++_currentPatrolPointIndex % _patrolPointsNumber;
                    _currentTarget = _patrolPoints[_currentPatrolPointIndex];
                }
            }
            else
            {
                _animator.SetBool("IsMoving_b", false);
                _targetAttacker.enabled = true;
            }
        }
        else if (_patrolPointsNumber > 0)
        {
            _currentTarget = _patrolPoints[_currentPatrolPointIndex];
        }
        else
        {
            _animator.SetBool("IsMoving_b", false);
            _targetAttacker.enabled = false; 
        }
    }

    public void SetPatrolPoints(Target[] points)
    {
        _patrolPoints = points;
    }

    public void SetTarget(Target target) 
    { 
        _currentTarget = target;
    }

    public Character GetReachedCharacter() 
    {
        _currentTarget.TryGetComponent<Character>(out Character character);

        return character;
    }
}