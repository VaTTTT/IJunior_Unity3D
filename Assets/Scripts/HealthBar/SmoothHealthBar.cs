using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthBar : HealthBar
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private float _changingSpeed;

    private float _sliderTargetValue;
    private float _sliderCurrentValue;

    private void Start()
    {
        _sliderCurrentValue = _healthSlider.maxValue;
    }

    protected override void OnHealthChanged(float currentValue)
    { 
        _sliderTargetValue = currentValue / Health.MaximalValue;

        StopCoroutine(nameof(SliderValueSmoothChanger));
        StartCoroutine(SliderValueSmoothChanger(_changingSpeed));
    }

    private IEnumerator SliderValueSmoothChanger(float speed)
    {
        while (_sliderCurrentValue != _sliderTargetValue)
        {
            _sliderCurrentValue = Mathf.MoveTowards(_sliderCurrentValue, _sliderTargetValue, speed * Time.deltaTime);

            _healthSlider.value = _sliderCurrentValue;

            yield return null;
        }
    }
}
