using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SurvivorsBase : Target
{
    [SerializeField] private LayerMask _resourcesLayerMask;
    [SerializeField] private float _resourcesScanDistance;
    [SerializeField] private float _resourcesScanDelay;
    [SerializeField] private WayPoint _survivorsSpawnPoint;
    [SerializeField] private WayPoint _cargoDropPoint;
    [SerializeField] private GameObject _survivorsContainer;
    [SerializeField] private Survivor _survivorPrefab;
    [SerializeField] private int _survivorsInitialNumber;
    [SerializeField] private int _cost;
    [SerializeField] private NewBaseFlag _newBaseFlagPrefab;

    private List<Survivor> _survivors = new();
    private List<Resource> _availableResources;
    private int _resourceQuantity;
    private Collider[] _resourceColliders;
    private Target _closestTarget;
    private bool _isSearchingResourcesState = true;
    private bool _isBuildingState = false;
    private Coroutine _baseBuildingRoutine;
    private NewBaseFlag _newBaseFlag;

    public WayPoint CargoDropPoint => _cargoDropPoint;
    public int Cost => _cost;

    private void Start()
    {
        Deselect();
        AddSurvivors(_survivorsInitialNumber);
        StartCoroutine(nameof(GatheringResources), _resourcesScanDelay);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Survivor survivor))
        {
            if (survivor.IsFree)
            {
                EncloseSurvivor(survivor);
            }
        }
    }

    public override void Select()
    {
        _isSelected = true;
        StartCoroutine(nameof(PlacingNewBaseFlag));

        if (_frame != null)
        {
            _frame.SetActive(true);
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

    public void AddResource()
    {
        _resourceQuantity++;

        if (!_isBuildingState)
        {
            TryBuySurvivor();
        }
    }

    public void AddSurvivor(Survivor survivor)
    {
        _survivors.Add(survivor);
        survivor.transform.parent = _survivorsContainer.transform;
    }

    private IEnumerator BuildingNewBase()
    {
        float delay = 1f;
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

        while (_newBaseFlag != null && _newBaseFlag.IsFree)
        {

            if (_resourceQuantity >= _cost)
            {
                Survivor freeSurvivor = TryGetFreeSurvivor();

                if (freeSurvivor != null)
                {
                    SendSurvivor(freeSurvivor, _newBaseFlag);
                    _survivors.Remove(freeSurvivor);
                    _resourceQuantity -= _cost;
                    _isBuildingState = false;
                    _isSearchingResourcesState = true;
                    StartCoroutine(nameof(GatheringResources), _resourcesScanDelay);
                }
            }

            yield return waitForSeconds;
        }
    }

    private IEnumerator PlacingNewBaseFlag()
    {
        float maximalPlacementHeight = 0.3f;
        float rayHitDistance = 200;
        LayerMask interactableLayerMask = LayerMask.GetMask("Ground", "Base");

        while (_isSelected)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Vector3 _mousePosition = Mouse.current.position.ReadValue();
                Ray _ray = Camera.main.ScreenPointToRay(_mousePosition);

                if (Physics.Raycast(_ray, out RaycastHit hit, rayHitDistance, interactableLayerMask))
                {
                    if (hit.point.y <= maximalPlacementHeight)
                    {
                        if (_baseBuildingRoutine != null)
                        {
                            StopCoroutine(_baseBuildingRoutine);
                        }

                        if (_newBaseFlag == null)
                        {
                            _newBaseFlag = Instantiate(_newBaseFlagPrefab, hit.point, transform.rotation);

                        }
                        else
                        {
                            _newBaseFlag.transform.position = hit.point;
                        }

                        _baseBuildingRoutine = StartCoroutine(nameof(BuildingNewBase));
                        _isBuildingState = true;
                    }
                }
            }

            yield return null;
        }
    }

    private void TryBuySurvivor()
    {
        if (_resourceQuantity >= _survivorPrefab.Cost)
        {
            Survivor survivor = Instantiate(_survivorPrefab, _survivorsSpawnPoint.transform.position, _survivorsSpawnPoint.transform.rotation, _survivorsContainer.transform);
            _resourceQuantity -= survivor.Cost;
            survivor.SetHomeBase(this);
            _survivors.Add(survivor);
            survivor.Release();
            survivor.gameObject.SetActive(false);
        }
    }

    private Resource TryGetFreeResource()
    {
        _availableResources = GetAvailableResources(_resourcesLayerMask, _resourcesScanDistance);
        Resource resource = _availableResources.FirstOrDefault(resource => resource.IsFree == true);
        return resource != null ? resource : null;
    }

    private List<Resource> GetAvailableResources(LayerMask searchLayer, float distance)
    {
        List<Resource> resources = new();

        _resourceColliders = Physics.OverlapSphere(transform.position, distance, searchLayer).OrderBy(collider => Vector3.Distance(collider.transform.position, transform.position)).ToArray();

        if (_resourceColliders.Length > 0)
        {
            foreach (Collider collider in _resourceColliders)
            {
                collider.TryGetComponent(out Resource resource);

                if (resource.IsFree)
                {
                    resources.Add(resource);
                }
            }
        }

        return resources;
    }

    private void AddSurvivors(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Survivor survivor = Instantiate(_survivorPrefab, _survivorsSpawnPoint.transform.position, _survivorsSpawnPoint.transform.rotation, _survivorsContainer.transform);
            survivor.SetHomeBase(this);
            _survivors.Add(survivor);
            survivor.Release();
            survivor.gameObject.SetActive(false);
        }
    }

    private void EncloseSurvivor(Survivor survivor)
    {
        survivor.SetMainTarget(null);
        survivor.SetTarget(null);
        survivor.gameObject.SetActive(false);
        survivor.transform.position = _survivorsSpawnPoint.transform.position;
    }

    private Survivor TryGetFreeSurvivor()
    {
        Survivor survivor = _survivors.FirstOrDefault(survivor => survivor.IsFree == true);
        return survivor != null ? survivor : null;
    }

    private void SendSurvivor(Survivor survivor, Target target)
    {
        target.Occupy();
        survivor.Occupy();
        survivor.SetMainTarget(target);
        survivor.gameObject.SetActive(true);
        survivor.gameObject.layer = _survivorPrefab.gameObject.layer;
    }

    private IEnumerator GatheringResources(float delay)
    {
        WaitForSeconds waitForSeconds = new(delay);
        Survivor freeSurvivor;
        Resource freeResource;

        while (_isSearchingResourcesState)
        {
            freeResource = TryGetFreeResource();

            if (freeResource != null)
            {
                freeSurvivor = TryGetFreeSurvivor();

                if (freeSurvivor != null)
                {
                    SendSurvivor(freeSurvivor, freeResource);
                }
            }

            yield return waitForSeconds;
        }
    }
}