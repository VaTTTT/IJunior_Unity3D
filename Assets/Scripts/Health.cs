using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maximalValue;
    
    private int _currentValue;

    public event UnityAction<int> Changed;
    public event UnityAction Died;

    public int MaximalValue => _maximalValue;
    public int CurrentValue => _currentValue;

    private void Start()
    {
        _currentValue = _maximalValue;
    }

    public void ApplyDamage(int damage)
    {
        if (damage > 0)
        {
            ChangeHealthValue(-damage);

            if (_currentValue <= 0)
            { 
                Died?.Invoke();
            }
        }
    }

    public void ApplyHealing(int amount)
    {
        if (amount > 0)
        {
            ChangeHealthValue(amount);
        }
    }

    private void ChangeHealthValue(int value)
    {
        _currentValue = Mathf.Clamp(_currentValue + value, 0, _maximalValue);

        Changed?.Invoke(_currentValue);
    }
}
