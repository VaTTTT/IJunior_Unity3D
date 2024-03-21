using UnityEngine;

public class Player : Character
{
    private void Awake()
    {
        EnemyLayerMask = LayerMask.GetMask("Enemy");
        MedicineLayerMask = LayerMask.GetMask("Medicine");
    }
}