using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private float detectionDistance;

    [SerializeField] private Transform target;

    [SerializeField] private LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        if (target)
        {
            transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up);
        }
        else
        {
            FindPlayer();
        }
    }

    void FindPlayer()
    {
        /*Physics.SphereCast(transform.position, detectionDistance, transform.forward,
            out RaycastHit hitinfo,detectionDistance,mask);
        target = hitinfo.transform;*/
        var a = Physics.OverlapSphere(transform.position, detectionDistance, mask);
        foreach (var col in a)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                target = col.gameObject.transform;
            }
        }
    }
}