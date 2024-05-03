using UnityEngine;

public class PatrolState : State
{
    [SerializeField] private float _speed;
    [SerializeField] private float _stopDistance;
    
    private WayPoint[] _patrolPoints;
    private int _currentPatrolPointIndex;
    private int _patrolPointsNumber;
    private Animator _animator;

    public int PatrolPointsNumber => _patrolPointsNumber;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.SetBool("IsMoving_b", true);
    }

    private void OnDisable()
    {
        _animator.SetBool("IsMoving_b", false);
    }

    private void Start()
    {
        _currentPatrolPointIndex = 0;
    }

    private void Update()
    {
        if (_patrolPoints != null)
        {
            if (Vector3.Distance(transform.position, _patrolPoints[_currentPatrolPointIndex].transform.position) > _stopDistance)
            {
                transform.LookAt(_patrolPoints[_currentPatrolPointIndex].transform.position);
                transform.Translate(_speed * Time.deltaTime * Vector3.forward);
            }
            else if (_patrolPointsNumber > 1)
            {
                _currentPatrolPointIndex = ++_currentPatrolPointIndex % _patrolPointsNumber;
            }
            else
            {
                _animator.SetBool("IsMoving_b", false);
            }
        }
    }

    public void SetPatrolPoints(WayPoint[] points)
    {
        _patrolPoints = points;
        _patrolPointsNumber = _patrolPoints.Length;
    }
}
