using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinder : TargetFinder
{
    private void Start()
    {
        _targetsLayerMask = LayerMask.GetMask("Player");
        _characterMover = GetComponent<CharacterMover>();
    }
}
