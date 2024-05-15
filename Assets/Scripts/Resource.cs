using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Target
{
    public override void Select()
    {
        _isSelected = false;

        if (_frame != null)
        {
            _frame.SetActive(false);
        }
    }

    public override void Deselect()
    {
        _isSelected = false;

        if (_frame != null)
        {
            _frame.SetActive(false);
        }
    }
}