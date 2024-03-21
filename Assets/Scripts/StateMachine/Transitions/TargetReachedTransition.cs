using UnityEngine;

public class TargetReachedTransition : Transition
{
    private Character _character;
    private bool _isAttackable;

    private void Start()
    {
        _character = GetComponent<Character>();
    }

    protected override void ChangeState()
    {
        if (_character.Target)
        {
            if (_character.Target.TryGetComponent<Enemy>(out _) || _character.Target.TryGetComponent<Player>(out _))
            {
                _isAttackable = true;
            }
            else
            {
                _isAttackable = false;
            }

            if (_isAttackable)
            {
                if (Vector3.Distance(_character.Target.transform.position, transform.position) <= _character.AttackDistance)
                {
                    NeedTransit = true;
                }
            }
            else
            {
                if (Vector3.Distance(_character.Target.transform.position, transform.position) <= _character.StopDistance)
                {
                    NeedTransit = true;
                }
            }
        }
    }
}
