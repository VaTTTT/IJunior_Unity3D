using UnityEngine;

public class Survivor : Character
{
    [SerializeField] private CargoContainer _cargoContainer;

    [SerializeField] private bool _isBusy;
    private SurvivorsBase _homeBase;

    public bool IsBusy => _isBusy;

    private void Awake()
    {
        EnemyLayerMask = LayerMask.GetMask("Enemy");
        MedicineLayerMask = LayerMask.GetMask("Medicine");
    }

    private void OnEnable()
    {
        _cargoContainer.Loaded += DeliverCargo;
        _cargoContainer.Unloaded += Release;
    }

    private void OnDisable() 
    {
        _cargoContainer.Loaded -= DeliverCargo;
        _cargoContainer.Unloaded -= Release;
    }

    /*
    public void TakeResource(Resource resource)
    {
        SetMainTarget(resource);
    }
    */

    public void DeliverCargo()
    {
        SetMainTarget(_homeBase.CargoDropPoint);
    }

    public void SetHomeBase(SurvivorsBase survivorsBase)
    { 
        _homeBase = survivorsBase;
    }

    public void Occupy()
    { 
        _isBusy = true;
    }

    public void Release()
    {
        _isBusy = false;
    }
}