using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidKit : Item
{
    [SerializeField] private int _healAmount;

     private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);

        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.ApplyHealing(_healAmount);
            Destroy(gameObject);
        }
    }
}
