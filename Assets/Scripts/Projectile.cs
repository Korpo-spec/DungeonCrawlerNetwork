using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.Netcode;
using UnityEngine;

public class Projectile : NetworkBehaviour, ISpawnData
{
    public ProjectileSpawnData spawnData { get; set; }
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
        direction = spawnData.direction;
        
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(direction * Time.deltaTime, Space.World);
    }
}
