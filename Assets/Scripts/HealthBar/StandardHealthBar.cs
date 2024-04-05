using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StandardHealthBar : HealthBar
{
    [SerializeField] private Slider _healthSlider;

    protected override void OnHealthChanged(float currentValue)
    {
        if (_healthSlider != null)
        {
            _healthSlider.value = currentValue / Health.MaximalValue;
        }
    }
}
