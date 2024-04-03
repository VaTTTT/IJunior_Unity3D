using TMPro;
using UnityEngine;

public class TextHealthBar : HealthBar
{
    [SerializeField] private TextMeshProUGUI _textField;

    private void Start()
    {
        _textField.text = Health.MaximalValue + " / " + Health.MaximalValue;
    }

    protected override void OnHealthChanged(int currentValue)
    {
        if (_textField != null)
        {
            _textField.text = currentValue + " / " + Health.MaximalValue;
        }
    }
}
