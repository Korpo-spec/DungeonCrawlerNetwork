using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.Netcode;
using UnityEngine;

public class Projectile : NetworkBehaviour, ISpawnData
{
    [SerializeField] private float speed = 1f;
    public ProjectileSpawnData spawnData { get; set; }

    [SerializeField] private GameObject projectileEffect;
    [SerializeField] private GameObject hit;
    [SerializeField] private GameObject flash;
    public void DeserializeSpawnData(string data)
    {
        spawnData = JsonUtility.FromJson<ProjectileSpawnData>(data);
        
    }

    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)return;
        direction = spawnData.direction;
        
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        if (flash !=null)
        {
            EnableFlashServerRpc();
            flash.SetActive(true);
        }

        Destroy(gameObject, 5f);
    }

    

    [ServerRpc]
    private void EnableFlashServerRpc()
    {
        EnableFlashClientRpc();
    }

    [ClientRpc]
    private void EnableFlashClientRpc()
    {
        flash.SetActive(true);
    }

    
    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)return;
        
            
        
        transform.Translate(direction * Time.deltaTime* speed, Space.World);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!IsOwner) return;
        speed = 0;
        
        ContactPoint contact = other.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point + contact.normal * 0.1f;
        hit.transform.position = pos;
        hit.transform.LookAt(contact.point + contact.normal);
        EnableHitServerRpc();
        hit.SetActive(true);
        projectileEffect.SetActive(false);
    }
    
    [ServerRpc]
    private void EnableHitServerRpc()
    {
        EnableHitClientRpc();
    }

    [ClientRpc]
    private void EnableHitClientRpc()
    {
        hit.SetActive(true);
        projectileEffect.SetActive(false);
    }
}
