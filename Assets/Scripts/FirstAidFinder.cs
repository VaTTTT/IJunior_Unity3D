using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidFinder : TargetFinder
{
    private void Start()
    {
        _itemsLayerMask = LayerMask.GetMask("Items");
        _characterMover = GetComponent<CharacterMover>();
    }
}
