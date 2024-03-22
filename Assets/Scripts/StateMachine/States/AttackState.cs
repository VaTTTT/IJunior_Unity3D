using UnityEngine;

public class AttackState : State
{
    [SerializeField] private float _attackSpeed;
    [SerializeField] private int _damage;

    private Character _character;
    private Animator _animator;
    private float _attackTimeCounter;
    private float _animationLength;
    private float _defaultAnimatorSpeed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.SetBool("IsAttacking_b", true);
        _animator.speed = _attackSpeed;
        _animationLength = 1.167f / _attackSpeed;
        _attackTimeCounter = _animationLength / 2;
    }

    private void OnDisable()
    {
        _animator.SetBool("IsAttacking_b", false);
        _animator.speed = _defaultAnimatorSpeed;
    }

    private void Start()
    {
        _defaultAnimatorSpeed = 1;
        _character = GetComponent<Character>();
    }

    private void Update()
    {
        _attackTimeCounter += Time.deltaTime;

        if (_attackTimeCounter >= _animationLength)
        {
            if (_character.Target.TryGetComponent<Character>(out Character target))
            {
                target.ApplyDamage(_damage);
            }

            _attackTimeCounter = 0;
        }
    }
}
