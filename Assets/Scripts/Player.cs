using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity,IDamageable,IResourceable
{
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; }
    public int MaxResource { get; set;}
    public int CurrentResource { get; }
    
    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    public bool TryCastAbility(int currentResource, Ability abilityToCast)
    {
        throw new System.NotImplementedException();
    }
}
