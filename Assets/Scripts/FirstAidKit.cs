using UnityEngine;

public class FirstAidKit : Item
{
    [SerializeField] private int _healAmount;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health))
        {
            health.ApplyHealing(_healAmount);
            Destroy(gameObject);
        }
    }

    public override void Select()
    {
        _isSelected = false;

        if (_frame != null)
        {
            _frame.SetActive(false);
        }
    }

    public override void Deselect()
    {
        _isSelected = false;

        if (_frame != null)
        {
            _frame.SetActive(false);
        }
    }
}