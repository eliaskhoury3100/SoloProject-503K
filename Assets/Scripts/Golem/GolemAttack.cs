using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAttack : MonoBehaviour
{
    private PlayerHealthSystem playerHealthSystem;


    [SerializeField] private const float attackWeight = 2;
    private bool _canDamage = false;

    // Start is called before the first frame update
    private void Start()
    {
        playerHealthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthSystem>();
    }
    private void Update()
    {
        Attack();
    }

    public void Attack()
    {
        if (_canDamage && playerHealthSystem != null)
        {
            Debug.Log("Player Hit!");
            playerHealthSystem.TakeDamage(attackWeight);
        }
    }

    public void EnableDamage()
    {
        _canDamage = true;
        //Debug.Log("Damage enabled!");
    }

    public void DisableDamage()
    {
        _canDamage = false;
        //Debug.Log("Damage disabled!");
    }
}
