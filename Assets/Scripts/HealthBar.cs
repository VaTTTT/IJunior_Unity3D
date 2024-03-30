using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private bool _isSmooth;
    [SerializeField] private int _barChangingSpeed;
    [SerializeField] private Health _object;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private TextMeshProUGUI _healthValue;
    [SerializeField] private Camera _mainCamera;

    private float _previousHealthPercentage = 100;
    private float _currentHealthPercentage = 100;
    private float _maximalHealthPercentage = 100;

    private void OnEnable()
    {
        _object.HealthChanged += OnHealthChanged;
        _healthValue.text = _currentHealthPercentage + " / " + _maximalHealthPercentage;
    }

    private void OnDisable()
    {
        _object.HealthChanged -= OnHealthChanged;
    }

    private void Update()
    {
        if (_isSmooth)
        {
            _previousHealthPercentage = Mathf.MoveTowards(_previousHealthPercentage, _currentHealthPercentage, _barChangingSpeed * Time.deltaTime);
            _healthBar.value = _previousHealthPercentage;
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + _mainCamera.transform.forward);
    }

    private void OnHealthChanged(int previousValue, int currentValue)
    {
        _previousHealthPercentage = previousValue * 100 / _object.MaximalHealthValue; 
        _currentHealthPercentage = currentValue * 100 / _object.MaximalHealthValue;

        if (_healthBar != null)
        {
            if (!_isSmooth)
            {
                _healthBar.value = _currentHealthPercentage;
            }
        }

        if (_healthValue != null)
        {
            _healthValue.text = _currentHealthPercentage + " / " + _maximalHealthPercentage;
        }
    }
}
