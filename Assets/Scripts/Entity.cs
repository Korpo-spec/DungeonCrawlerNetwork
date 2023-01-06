using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class Entity : NetworkBehaviour
{
    public int MaxResource { get; private set; }
    public int CurrentResource { get; private set; }

    public int MaxHealth { get; private set; }

    public int CurrentHealth { get; private set; }

    public void UseResource(int resource)
    {
        CurrentResource -= resource;
    }
    public void RestoreResource(int resource)
    {
        CurrentResource += resource;
        if (CurrentResource > MaxResource) 
        {
            CurrentResource = MaxResource;
        }
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
    }
    public void TakeHealing(int heal)
    {
        CurrentHealth -= heal;
        if (CurrentHealth > MaxHealth) 
        {
            CurrentHealth = MaxHealth;
        }
    }
    
    public bool TryCastAbility(Ability abilityToCast)
    {
        throw new System.NotImplementedException();
    }
}
