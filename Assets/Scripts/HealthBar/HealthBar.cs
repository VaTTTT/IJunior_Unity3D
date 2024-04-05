using UnityEngine;

public abstract class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;    
    [SerializeField] private Camera _mainCamera;

    protected Health Health => _health;

    private void OnEnable()
    {
        _health.Changed += OnHealthChanged;

        if (!_mainCamera)
        {
            _mainCamera = Camera.main;
        }
    }

    private void OnDisable()
    {
        _health.Changed -= OnHealthChanged;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + _mainCamera.transform.forward);
    }

    protected abstract void OnHealthChanged(float currentValue);
}
