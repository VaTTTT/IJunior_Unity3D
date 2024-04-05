using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maximalValue;
    
    private float _currentValue;

    public event UnityAction<float> Changed;
    public event UnityAction Died;

    public float MaximalValue => _maximalValue;
    public float CurrentValue => _currentValue;

    private void Start()
    {
        _currentValue = _maximalValue;
    }

    public void ApplyDamage(float damage)
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

    public void ApplyHealing(float amount)
    {
        if (amount > 0)
        {
            ChangeHealthValue(amount);
        }
    }

    private void ChangeHealthValue(float value)
    {
        _currentValue = Mathf.Clamp(_currentValue + value, 0, _maximalValue);

        Changed?.Invoke(_currentValue);
    }
}
