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
}
