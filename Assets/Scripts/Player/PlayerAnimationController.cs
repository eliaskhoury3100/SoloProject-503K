using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimationController : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public Vector3 playerPosition;
    }

    private Animator _animator;
    private NavMeshAgent _agent;
    private PlayerCombat _playerCombat;


    [SerializeField] private float runSpeed = 3.5f;
    [SerializeField] private float sprintSpeed = 5f;

    private float idleTimer = 0f;
    [SerializeField] private float idleTimerThreshold = 10.0f;


    private const string RUN_ANIM_PARAM = "run";
    private const string SPRINT_ANIM_PARAM = "sprint";
    private const string RELAX_ANIM_TRIGGER = "relax";
    private const string HIT1_ANIM_TRIGGER = "hit1";
    private const string HIT2_ANIM_TRIGGER = "hit2";
    private const string HIT3_ANIM_TRIGGER = "hit3";


    // Start is called before the first frame update
    void Start()
    {
        if (!_animator)
            _animator = this.GetComponent<Animator>();
        
        if (!_agent)
            _agent = this.GetComponent<NavMeshAgent>();
        runSpeed = _agent.speed;

        if (!_playerCombat)
            _playerCombat = this.GetComponent<PlayerCombat>();
    }

    void StopMovement()
    {
        _animator.SetBool(RUN_ANIM_PARAM, false);
        _animator.SetBool(SPRINT_ANIM_PARAM, false);

        // Stop the NavMeshAgent from moving
        _agent.isStopped = true; // Stop the agent
        _agent.ResetPath();      // Clear the current path to remove the destination
    }

    private void LateUpdate()
    {
        // fix issue with duplicating hit animation when trigger not resetting correctly!
        _animator.ResetTrigger(HIT1_ANIM_TRIGGER); 
        _animator.ResetTrigger(HIT2_ANIM_TRIGGER);
        _animator.ResetTrigger(HIT3_ANIM_TRIGGER); 
    }

    // Update is called once per frame
    void Update()
    {
        // player combatting hit1
        if ((Input.GetKey(KeyCode.X) || Input.GetMouseButtonDown(1)) && _playerCombat.canAttackHit1) // X or right mouse button
        {
            idleTimer = 0; // reset the timer
            StopMovement();
            _animator.SetTrigger(HIT1_ANIM_TRIGGER);
        }
        // player combatting hit2
        if (Input.GetKey(KeyCode.Y) && _playerCombat.canAttackHit2)
        {
            idleTimer = 0; // reset the timer
            StopMovement();
            _animator.SetTrigger(HIT2_ANIM_TRIGGER);
        }
        // player combatting hit3
        if (Input.GetKey(KeyCode.M) && _playerCombat.canAttackHit3)
        {
            idleTimer = 0; // reset the timer
            StopMovement();
            _animator.SetTrigger(HIT3_ANIM_TRIGGER);
        }


        // player interrupts combat to move to mouse click
        if (Input.GetMouseButtonDown(0))
        {
            _animator.CrossFade("Run", 0.1f); // implements interrupting the triggered hit animation
        }


        // player moving 
        if (_agent.velocity.magnitude > 0)
        {
            idleTimer = 0; // reset the timer
            _animator.SetBool(RUN_ANIM_PARAM, true);
            _agent.speed = runSpeed;
            // player sprinting
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                _animator.SetBool(SPRINT_ANIM_PARAM, true);
                _agent.speed = sprintSpeed; // set to sprint speed
            }
            else
            {
                _animator.SetBool(SPRINT_ANIM_PARAM, false);
                _agent.speed = runSpeed; // set to run speed
            }
        }
        // player idle
        else
        {
            _animator.SetBool(RUN_ANIM_PARAM, false);

            // check relax state if idle for more than the threshold or key R is down
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleTimerThreshold || Input.GetKey(KeyCode.R))
            {
                idleTimer = 0; // reset the timer
                // Trigger the relax animation
                _animator.SetTrigger(RELAX_ANIM_TRIGGER);
            }
        }
    }

    public Data Save()
    {
        return new Data
        {
            playerPosition = this.transform.position,
        };
    }

    public void Load(Data data)
    {
        this.transform.position = data.playerPosition;
    }

}
