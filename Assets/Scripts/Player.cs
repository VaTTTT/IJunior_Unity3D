using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private void Awake()
    {
        EnemyLayerMask = LayerMask.GetMask("Enemy");
        MedicineLayerMask = LayerMask.GetMask("Medicine");
    }
}