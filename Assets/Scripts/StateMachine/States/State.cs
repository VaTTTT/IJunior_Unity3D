using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    public void Enter()
    {
        if (enabled == false)
        {
            enabled = true;
            Debug.Log("State is enabled");

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                Debug.Log("Transitions are enabled");
            }
        }
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
            {
                transition.enabled = false;
            }

            enabled = false;
        }
    }

    public State GetNextState()
    { 
        foreach(var transition in _transitions) 
        {
            if (transition.NeedTransit)
            { 
                return transition.TargetState;
            }
        }

        return null;
    }
}
