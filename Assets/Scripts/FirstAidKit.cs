using UnityEngine;

public class FirstAidKit : Item
{
    [SerializeField] private int _healAmount;

     private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.ApplyHealing(_healAmount);
            Destroy(gameObject);
        }
    }
}
