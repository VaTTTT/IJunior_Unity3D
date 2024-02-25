using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Target[] _patrolPoints;
    [SerializeField] private Target _currentTarget;
    [SerializeField] private float _stopDistance;
    
    private Animator _animator;
    private int _currentPatrolPointIndex;
    private int _patrolPointsNumber;
    private Vector3 _targetPosition;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _patrolPointsNumber = _patrolPoints.Length;
        _currentPatrolPointIndex = 0;
    }

    private void Update()
    {
        if (_currentTarget != null)
        {
            _animator.SetFloat("Speed_f", _speed);

            _targetPosition = _currentTarget.gameObject.transform.position;

            if (Vector3.Distance(transform.position, _targetPosition) > _stopDistance)
            {
                transform.LookAt(_targetPosition);
                transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            }
            else if(_patrolPointsNumber > 0)
            {
                _currentTarget = GetNextTarget();
            }

        }
        else if (_patrolPointsNumber > 0)
        {
            _currentTarget = _patrolPoints[_currentPatrolPointIndex];
        }
    }

    private Target GetNextTarget()
    {
        if (_patrolPointsNumber > 0)
        {
            if (_currentPatrolPointIndex < _patrolPointsNumber - 1)
            {
                _currentPatrolPointIndex++;
            }
            else
            {
                _currentPatrolPointIndex = 0;
            }
            
            return _patrolPoints[_currentPatrolPointIndex];
        }

       return null;
    }

    public void SetPatrolPoints(Target[] points)
    {
        _patrolPoints = points;
    }

    public void SetTarget(Target target) 
    { 
        _currentTarget = target;
    }
}