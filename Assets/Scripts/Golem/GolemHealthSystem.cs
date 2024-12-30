using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemHealthSystem : MonoBehaviour
{

	public float maxHealth = 100;
	public float currentHealth;

	public HealthBar healthBar;

	private float COUNT_DOWN_TILL_DESTROYED = 5; 

	void Start()
	{
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
	}

	public void TakeDamage(float damage)
	{
		currentHealth -= damage;

		healthBar.SetHealth(currentHealth);

		if (currentHealth <= 0)
		{
			Die();
		}
	}

	// Handle Golum death TO MODIFY!!!
	private void Die()
	{
		Debug.Log("Golum has died!");
		Destroy(this.gameObject, COUNT_DOWN_TILL_DESTROYED);
	}
}