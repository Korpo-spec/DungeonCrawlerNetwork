using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int MaxHealth { get; set; }
    int CurrentHealth { get; }
    void TakeDamage(int damage);
}
