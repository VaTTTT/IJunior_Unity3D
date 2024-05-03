using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Target
{
    private bool _isFree = true;

    public bool IsFree => _isFree;

    public void Occupy()
    { 
        _isFree = false;
        gameObject.layer = 0;
    }
}