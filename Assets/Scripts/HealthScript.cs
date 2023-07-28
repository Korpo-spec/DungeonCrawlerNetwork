using System;
using System.Collections;
using System.Collections.Generic;
using QFSW.QC;
using Unity.Netcode;
using UnityEngine;

public class HealthScript : MonoBehaviour
{

    [SerializeField] private int maxHealth;

    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    [Command()]
    protected virtual void OnDeath()
    {
        OnDeathServerRpc();
        //Destroy(this.gameObject);
    }

    [ServerRpc]
    private void OnDeathServerRpc()
    {
        //OnDeathClientRpc();
        GetComponent<NetworkObject>().Despawn();
    }

    [ClientRpc]
    private void OnDeathClientRpc()
    {
        OnDeath();
    }

    public virtual void Damage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }

    public virtual void Heal(int heal)
    {
        currentHealth += heal;
        if (currentHealth >maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    
}
