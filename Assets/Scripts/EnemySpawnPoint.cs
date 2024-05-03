using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private WayPoint[] _patrolPoints;

    public Enemy Enemy => _enemy;
    public WayPoint[] PatrolPoints => _patrolPoints;
}