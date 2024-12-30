using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public bool canAttackHit1;
    public bool canAttackHit2;
    public bool canAttackHit3;

    [SerializeField] private float PERCENT_MANA_REQUIRED_HIT1 = 0;
    [SerializeField] private float PERCENT_MANA_REQUIRED_HIT2 = 0.5f;
    [SerializeField] private float PERCENT_MANA_REQUIRED_HIT3 = 0.99f;

    [SerializeField] private float MANA_USAGE_HIT1 = 0.2f;
    [SerializeField] private float MANA_USAGE_HIT2 = 0.7f;
    [SerializeField] private float MANA_USAGE_HIT3 = 1;

    [SerializeField] private float DAMAGE_HIT1 = 1f;
    [SerializeField] private float DAMAGE_HIT2 = 1.5f;
    [SerializeField] private float DAMAGE_HIT3 = 2f;

    private bool _canDamage = false;
    private const float _damageRange = 1.5f;

    [SerializeField] private PlayerManaSystem playerManaSystem;

    void Start()
    {
        if (!playerManaSystem)
            this.GetComponent<PlayerManaSystem>();
    }

    void Update()
    {
        CheckAttackMana();
        Attack();
    }

    private void CheckAttackMana()
    {
        canAttackHit1 = playerManaSystem.CanAttack(PERCENT_MANA_REQUIRED_HIT1);
        canAttackHit2 = playerManaSystem.CanAttack(PERCENT_MANA_REQUIRED_HIT2);
        canAttackHit3 = playerManaSystem.CanAttack(PERCENT_MANA_REQUIRED_HIT3);
    }

    private void Attack()
    {
        if (_canDamage)
        {
            GolemHealthSystem targetGolem = GetClosestGolem();
            if (targetGolem != null)
            {
                Transform golemTransform = targetGolem.transform;
                float distance = Vector3.Distance(this.transform.position, golemTransform.position);
                if (distance <= _damageRange)
                {
                    if (canAttackHit1)
                    {
                        playerManaSystem.UseMana(MANA_USAGE_HIT1);
                        targetGolem.TakeDamage(DAMAGE_HIT1);
                    }
                    else if (canAttackHit2)
                    {
                        playerManaSystem.UseMana(MANA_USAGE_HIT2);
                        targetGolem.TakeDamage(DAMAGE_HIT2);
                    }
                    else if (canAttackHit3)
                    {
                        playerManaSystem.UseMana(MANA_USAGE_HIT3);
                        targetGolem.TakeDamage(DAMAGE_HIT3);
                    }
                }
            }
        }
    }

    private GolemHealthSystem GetClosestGolem()
    {
        GameObject[] golems = GameObject.FindGameObjectsWithTag("Golem");
        GolemHealthSystem closestGolem = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject golem in golems)
        {
            float distance = Vector3.Distance(this.transform.position, golem.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestGolem = golem.GetComponent<GolemHealthSystem>();
            }
        }

        return closestGolem;
    }

    public void EnableDamage()
    {
        _canDamage = true;
        Debug.Log("Inside EnableDamage, _canDamage is" + _canDamage);
        StartCoroutine("WaitASecond");
        Debug.Log("Damage enabled!");
    }

    IEnumerator WaitASecond()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Inside Coroutine, _canDamage is" + _canDamage);
        _canDamage = false;
        yield return null;
    }

    // no longer used, but still is in the AnimationEvent, so I have to keep it to avoid error
    public void DisableDamage()
    {
        //_canDamage = false;
        Debug.Log("Damage disabled!");
    }
    
}
