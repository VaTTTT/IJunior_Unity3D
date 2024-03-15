using UnityEngine;

public class Enemy : Character
{
    private void Awake()
    {
        EnemyLayerMask = LayerMask.GetMask("Player");
    }
}