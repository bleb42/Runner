using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _chaseSpeed = 2f;
    [SerializeField] private float _patrolWaitTime = 3f;
    [SerializeField] private Transform[] _targetPositions;
    [SerializeField] private float _minimumDistanceToStop = 0.1f;
    private NavMeshAgent _agent;
    public bool IsHavePatron = false;
    private bool _isChasingPlayer = false; 


    public EnemySense enemy;

    public float EnemyHp = 200;
    public GameObject _targetPlayer;


    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;

        StartCoroutine(Patrol());
    }

    private void Death()
    {
        if(EnemyHp <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (enemy != null && enemy.IfSeenYou && !_isChasingPlayer || IsHavePatron == true)  
        {
            _isChasingPlayer = true;
            StartCoroutine(ChasePlayer());
        }
        Death();
    }

    private IEnumerator ChasePlayer()
    {
        while (enemy.IfSeenYou) 
        {
            _agent.speed = _chaseSpeed;
            _agent.SetDestination(_targetPlayer.transform.position);

            while (_agent.pathPending || _agent.remainingDistance > _minimumDistanceToStop)
            {
                yield return null;
            }

            yield return null;
        }

        _isChasingPlayer = false; 
        StartCoroutine(Patrol());  
    }

    private IEnumerator Patrol()
    {
        while (!_isChasingPlayer || !enemy.IfSeenYou) 
        {
            int randomTarget = Random.Range(0, _targetPositions.Length);
            _agent.SetDestination(_targetPositions[randomTarget].position);

            while (_agent.pathPending || _agent.remainingDistance > _minimumDistanceToStop)
            {
                yield return null;
            }

            yield return new WaitForSeconds(_patrolWaitTime);
        }
    }
}
