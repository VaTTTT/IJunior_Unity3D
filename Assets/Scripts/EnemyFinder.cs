using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFinder : TargetFinder
{
    private void Start()
    {
        _targetsLayerMask = LayerMask.GetMask("Enemy");
        _itemsLayerMask = LayerMask.GetMask("Items");
        _characterMover = GetComponent<CharacterMover>();
    }
}
