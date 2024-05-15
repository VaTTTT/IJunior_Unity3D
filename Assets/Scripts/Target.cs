using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public abstract class Target : MonoBehaviour
{
    [SerializeField] protected GameObject _frame;

    protected bool _isFree = true;
    protected bool _isSelected = false;

    public bool IsFree => _isFree;

    public abstract void Select();

    public abstract void Deselect();


    public void Occupy()
    {
        _isFree = false;
        gameObject.layer = 0;
    }
}
