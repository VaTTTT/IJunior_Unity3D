using UnityEngine;

public class MoveState : State
{
    [SerializeField] private float _speed;

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
        if (_character.Target != null)
        {
            transform.LookAt(_character.Target.transform.position);
            transform.Translate(_speed * Time.deltaTime * Vector3.forward);
        }
    }
}
