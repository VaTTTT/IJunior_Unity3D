using UnityEngine;

[RequireComponent(typeof(PatrolState))]
public class PatrolTransition : Transition
{
    private PatrolState _patrolState;

    private void Start()
    {
        _patrolState = GetComponent<PatrolState>();
    }

    protected override void ChangeState()
    {
        if (_patrolState.PatrolPointsNumber > 0)
        {
            NeedTransit = true;
        }
    }
}
