using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private StateController controller;

    [SerializeField] private Ability ability1;

    [SerializeField] private Dictionary<string, ClassContainer> _classes;
    [SerializeField] private ClassContainer _container;

    private void Awake()
    {
        _classes = new Dictionary<string, ClassContainer>();
        _classes.Add("Void", _container);
    }

    // Start is called before the first frame update
    void Start()
    {
        
        SetClassServer("Void");
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CastAbility(ability1);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            
            return;
        }
    }
    
    
    private void CastAbility(Ability ability)
    {
        if (controller.highestPriority<= ability.abilityCode.priority)
        {
            ability.OnAbilityCast(controller);
        }
       
    }

    
    public void SetClassServer(FixedString64Bytes className)
    {
        GameObject classMesh = _classes[className.ToString()].mesh;

        
        for (int i = 0; i < classMesh.transform.childCount; i++)
        {
            var g = Instantiate(classMesh.transform.GetChild(i).gameObject);
            g.transform.parent = transform;
        }
        
        
        Debug.Log(transform.name, gameObject);
    }
}
