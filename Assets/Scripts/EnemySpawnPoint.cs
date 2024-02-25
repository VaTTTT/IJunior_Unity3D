using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Target _initialTarget;
    [SerializeField] private Target[] _patrolPoints;

    public Enemy Enemy => _enemy;
    public Target InitialTarget => _initialTarget;
    public Target[] PatrolPoints => _patrolPoints;
}