using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
    
    private List<Survivor> _survivors = new();
    private List<Resource> _availableResources;
    private int _resourceQuantity;
    private Collider[] _resourceColliders;
    private Target _closestTarget;
    private Coroutine _findResourcesRoutine;
    private bool _isSearchingForResources = true;

    public WayPoint CargoDropPoint => _cargoDropPoint;

    private void Start()
    {
        AddSurvivors(_survivorsInitialNumber);

        StartCoroutine(nameof(GatherResources), _resourcesScanDelay);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Survivor survivor))
        {
            if (!survivor.IsBusy)
            {
                EncloseSurvivor(survivor);
            }
        }
    }

    public void AddResource()
    {
        _resourceQuantity++;
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
            //_resourceColliders.OrderBy(collider => Vector3.Distance(collider.transform.position, transform.position));
            
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
        Survivor survivor = _survivors.FirstOrDefault(survivor => survivor.IsBusy == false);
        return survivor != null ? survivor : null;
    }

    private IEnumerator GatherResources(float delay)
    {
        WaitForSeconds waitForSeconds = new(delay);
        Survivor freeSurvivor;
        Resource freeResource;

        while (_isSearchingForResources)
        {
            freeResource = TryGetFreeResource();

            if (freeResource != null)
            {
                freeSurvivor = TryGetFreeSurvivor();

                if (freeSurvivor != null)
                { 
                    freeResource.Occupy();
                    freeSurvivor.Occupy();
                    freeSurvivor.SetMainTarget(freeResource);
                    freeSurvivor.gameObject.SetActive(true);
                }
            }

            yield return waitForSeconds;
        }
    }
}
