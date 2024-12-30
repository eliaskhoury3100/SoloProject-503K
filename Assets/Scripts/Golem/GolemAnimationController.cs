using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemAnimationController : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public string ID = string.Empty;
        public bool isSlayed = false;
        public Vector3 golemPosition;
    }
    public string ID => gameObject.GetInstanceID().ToString(); // given by Unity at instantiation
    bool isSlayed = false;

    private Animator _anim; // AnimatorController
    private const string PATROL_ANIM_PARAM = "isPatrolling";
    private const string CHASE_ANIM_PARAM = "isChasing";
    private const string ATTACK_ANIM_PARAM = "isAttacking";
    private const string DEAD_ANIM_TRIGGER = "isDead";

    private PlayerDetectionConeVision _playerDetected; // Reference to the PlayerDetectionConeVision script

    [SerializeField] private Transform[] wayPoints;  // Assign waypoints in the Unity editor

    private NavMeshAgent _agent;
    private const float _stoppingDistance = 2.0f;
    [SerializeField] private float patrolSpeed = 1.5f;
    [SerializeField] private float chaseSpeed = 2.5f;
    [SerializeField] private float attackDistance = 4.0f;

    private int _currentDestinationPoint; // index

    private Transform _playerTransform;
    private bool _chasingPlayer = false;
    private GolemHealthSystem _golemHealthSystem;

    private void StartPatrolling()
    {
        // initialize patrol
        _chasingPlayer = false;
        _anim.SetBool(CHASE_ANIM_PARAM, false);
        _anim.SetBool(ATTACK_ANIM_PARAM, false);
        GotoNextWaypoint();
    }

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
   
        _playerDetected = GetComponentInChildren<PlayerDetectionConeVision>();
        if (_playerDetected == null)
            Debug.LogError("No PlayerDetectionConeVision script found in child objects!");

        _anim = this.GetComponent<Animator>();
        _agent = this.GetComponent<NavMeshAgent>();
        _golemHealthSystem = this.GetComponent<GolemHealthSystem>();

        StartPatrolling();
    }

    private void GotoNextWaypoint()
    {
        if (wayPoints.Length == 0)
            return;

        // Choose a random waypoint
        _currentDestinationPoint = UnityEngine.Random.Range(0, wayPoints.Length);
        // Set the destination of the agent to the selected random waypoint
        _agent.SetDestination(wayPoints[_currentDestinationPoint].position);
        _agent.speed = patrolSpeed;
        _anim.SetBool(PATROL_ANIM_PARAM, true);
    }

    private void ResumePatrolling()
    {
        // Check if the agent has reached its destination
        if (!_agent.pathPending && _agent.remainingDistance <= _stoppingDistance)
            GotoNextWaypoint(); // Go to the next random waypoint
    }

    private void HandleDeath()
    {
        _agent.enabled = false;
        _anim.SetBool(PATROL_ANIM_PARAM, false);
        _anim.SetBool(CHASE_ANIM_PARAM, false);
        _anim.SetBool(ATTACK_ANIM_PARAM, false);
        _anim.SetTrigger(DEAD_ANIM_TRIGGER);
        isSlayed = true;
    }


    void Update()
    {
        // death condition
        if (_golemHealthSystem.currentHealth <= 0)
            HandleDeath();
        else
        {
            if (_playerDetected.playerDetected)
            {
                _chasingPlayer = true;
                ChasePlayer();
            }
            else
            {
                if (_chasingPlayer)
                    StartPatrolling();
                else
                    ResumePatrolling();
            }
        }
    }

    private void ChasePlayer()
    {
        _anim.SetBool(PATROL_ANIM_PARAM, false);
        _anim.SetBool(CHASE_ANIM_PARAM, true);
        if (_playerTransform != null && _agent.enabled == true)
        {
            _agent.SetDestination(_playerTransform.position);

            _agent.speed = chaseSpeed;

            if (Vector3.Distance(this.transform.position, _playerTransform.position) <= attackDistance)
            {
                _anim.SetBool(ATTACK_ANIM_PARAM, true);
                //Attack(); handled inside GolemAttack.cs script
            }
            else
            {
                _anim.SetBool(ATTACK_ANIM_PARAM, false);
            }
        }
    }

    public Data Save()
    {
        return new Data
        {
            ID = this.ID,
            isSlayed = this.isSlayed,
            golemPosition = this.transform.position
        };
    }

    public void Load(Data data)
    {
        isSlayed = data.isSlayed;
        this.transform.position = data.golemPosition;

        if (isSlayed)
            Destroy(this.gameObject);
        // no need for else because this.gameObject would be destroyed!
    }
}
