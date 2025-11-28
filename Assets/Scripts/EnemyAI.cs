using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent _agent; // Reference to the NavMeshAgent
    public Transform _player;   // Reference to player's transform
    public LayerMask _groundLayer, _playerLayer; // Layer for ground and player detection

    // Patrolling
    public Vector3 _patrolPoint;
    bool _patrolPointSet;
    public float _patrolRange;

    //Attacking
    public float _timeBetweenAttacks;
    bool _alreadyAttacked;

    //States
    public float _sightRange, _attackRange;
    public bool _playerInSightRange, _playerInAttackRange;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is in sight or attack range
        _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _playerLayer);
        _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _playerLayer);

        if (!_playerInSightRange && !_playerInAttackRange) Patrol();
        if (_playerInSightRange && !_playerInAttackRange)
            ChasePlayer();
        if (_playerInAttackRange) AttackPlayer();
    }

    void Patrol()
    {
        if (!_patrolPointSet) SearchPatrolPoint() ;
        if (_patrolPointSet)
        {
            _agent.SetDestination(_patrolPoint);
        }
        // Distance between patrol point and enemy
        Vector3 _distanceToPatrolPoint = transform.position - _patrolPoint;

        //Patrol point reached
        if (_distanceToPatrolPoint.magnitude < 0.5f) _patrolPointSet = false;
    }

    void SearchPatrolPoint()
    {
        // Random patrol point within a range
        float _randomZ = Random.Range(-_patrolRange, _patrolRange);
        float _randomX = Random.Range(-_patrolRange, _patrolRange);

        _patrolPoint = new Vector3(transform.position.x + _randomX, transform.position.y, transform.position.z + _randomZ);
        //nevmesh.sampleposition

        if (Physics.Raycast(_patrolPoint, -transform.up, 2f, _groundLayer))
            _patrolPointSet = true;
    }

    void ChasePlayer()
    {
        _agent.SetDestination(_player.position);
    }

    void AttackPlayer()
    {
        _agent.SetDestination(transform.position);
        if (!_alreadyAttacked)
        {
            Debug.Log("Enemy Attacked");
            _alreadyAttacked = true;
            Invoke(nameof(ResetAttack), _timeBetweenAttacks); //Attack CD;
        }
    }

    void ResetAttack()
    {
        Debug.Log("Enemy Attack CD Reseted");
        _alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _sightRange);
    }
}