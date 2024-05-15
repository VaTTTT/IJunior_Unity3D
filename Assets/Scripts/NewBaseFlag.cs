using UnityEngine;

public class NewBaseFlag : Target
{
    [SerializeField] private SurvivorsBase _basePrefab;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Survivor survivor) && survivor.MainTarget == this)
        {
            SurvivorsBase newBase = Instantiate(_basePrefab, transform.position, transform.rotation);
            //this.gameObject.SetActive(false);
            _isFree = true;
            survivor.SetHomeBase(newBase);
            survivor.SetMainTarget(null);
            survivor.SetTarget(null);
            survivor.Release();
            newBase.AddSurvivor(survivor);
            Destroy(gameObject);
        }
    }

    public override void Select()
    {
    }

    public override void Deselect()
    {
    }
}