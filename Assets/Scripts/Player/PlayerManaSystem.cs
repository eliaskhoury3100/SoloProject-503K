using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManaSystem : MonoBehaviour
{
    public float maxMana = 100f; // Maximum mana
    public float currentMana;   // Current mana
    public float manaRegenRate = 1f; // Mana regenerated per second

    public ManaBar manaBar;

    private void Start()
    {
        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);
    }
    private void Update()
    {
        RegenerateMana();
    }

    private void RegenerateMana()
    {
        if (currentMana < maxMana)
        {
            currentMana += manaRegenRate * Time.deltaTime; // Regen over time every 5 seconds
            currentMana = Mathf.Clamp(currentMana, 0, maxMana); // Ensure it doesn't exceed maxMana
        }
        manaBar.SetMana(currentMana);
    }

    public void UseMana(float amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;
            Debug.Log("Used " + amount + " mana!");
        }
        else
        {
            Debug.Log("Not enough mana!");
        }
        manaBar.SetMana(currentMana);
    }

    // Check if there's enough mana for hits based on percentage (0-0.5-1)
    public bool CanAttack(float percentage)
    {
        return currentMana > percentage*maxMana;
    }

}
