using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class LifeStealing : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private float _amount;
    [SerializeField] private float _time;

    private Collider[] _targetsColliders;
    private Target[] _targets;
    private LayerMask _targetLayerMask;
    private Health characterHealth;

    private Coroutine _stealLifeRoutine = null;
    private Coroutine _setTargetsRoutine = null;

    private void Start()
    {
        _targetLayerMask = LayerMask.GetMask("Enemy");
        characterHealth = GetComponent<Health>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            if (_setTargetsRoutine != null)
            {
                StopCoroutine(_setTargetsRoutine); 
            }           

            _setTargetsRoutine = StartCoroutine(SetTargets(_targetLayerMask, _distance, _time));
            
            if (_targets != null)
            {
                if (_stealLifeRoutine != null)
                { 
                    StopCoroutine(_stealLifeRoutine);
                }

                _stealLifeRoutine = StartCoroutine(StealLife(_targets, _amount, _time));
            }
        }
    }

    private IEnumerator SetTargets(LayerMask searchLayer, float distance, float time)
    {
        float timer = 0;

        while (timer <= time)
        {
            timer += Time.deltaTime;

            _targetsColliders = Physics.OverlapSphere(transform.position, distance, searchLayer);

            if (_targetsColliders.Length > 0)
            {
                _targets = _targetsColliders.Select(t => t.GetComponent<Target>()).ToArray();
            }

            yield return null;
        }
    }

    private IEnumerator StealLife(Target[] targets, float amount, float time)
    {
        float timer = 0;
        float lifeValue;

        while (timer <= time)
        {
            timer += Time.deltaTime;

            for (int i = 0; i < targets.Length; i++) 
            {
                targets[i].TryGetComponent(out Health targetHealth);
                
                if (targetHealth != null) 
                {
                    lifeValue = amount/time * Time.deltaTime;

                    if (targetHealth.CurrentValue > 0)
                    {
                        targetHealth.ApplyDamage(lifeValue);
                        characterHealth.ApplyHealing(lifeValue);
                    }
                }
            }

            yield return null;
        }
    }
}
