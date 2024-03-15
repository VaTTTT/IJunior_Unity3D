using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectTargetState : State
{
    protected LayerMask _targetsLayerMask;
    //protected LayerMask _itemsLayerMask;
    //protected CharacterMover _characterMover;

    //[SerializeField] private float _chaseDistance;
    //[SerializeField] private float _itemFindingDistance;
    //[SerializeField] private float _sightDistance;
    [SerializeField] private float _delay;

    private Collider[] _targetsColliders;
    //private Collider[] _itemsColliders;
    private Character _closestTarget;
    //private Item _closestItem;
    private Character _character;
    private float _timer;

    private void Awake()
    {
        _character = GetComponent<Character>(); 
    }

    private void OnEnable()
    {
        _timer = 0;
    }

    private void Start()
    {
        _targetsLayerMask = _character.EnemyLayerMask;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer <= 0) 
        {
            _timer = _delay;

            _targetsColliders = Physics.OverlapSphere(transform.position, _character.EnemyDetectDistance, _targetsLayerMask);
            
            if (_targetsColliders.Length > 0)
            {
                _closestTarget = _targetsColliders.OrderBy(target => Vector3.Distance(target.transform.position, transform.position)).FirstOrDefault().GetComponent<Character>();
                _character.SetTarget(_closestTarget);
            }
            else
            {
                _character.SetTarget(null);
            }
        }
        //_itemsColliders = Physics.OverlapSphere(transform.position, _itemFindingDistance, _itemsLayerMask);

        /*
        if (_character.CurrentHealth < _character.InitialHealth)
        {
            if (_itemsColliders.Length > 0)
            {
                _closestItem = GetClosestItem();

                if (Vector3.Distance(_closestItem.transform.position, transform.position) < _itemFindingDistance)
                {
                    _characterMover.SetTarget(_closestItem);
                    Debug.Log("Found Item");
                }
            }
        }
        */


    }

    /*
    private Target GetClosestTarget()
    {
        Target target;

        target = _targetsColliders.OrderBy(target => Vector3.Distance(target.transform.position, transform.position)).FirstOrDefault().GetComponent<Target>();

        return target;
    }
    */

    /*
    private Item GetClosestItem()
    {
        Item item;

        item = _itemsColliders.OrderBy(item => Vector3.Distance(item.transform.position, transform.position)).FirstOrDefault().GetComponent<Item>();

        return item;
    }
    */
}
