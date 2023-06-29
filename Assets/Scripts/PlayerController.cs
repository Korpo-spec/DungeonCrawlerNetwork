using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private StateController controller;

    [SerializeField] private Ability ability1;

    [SerializeField] private Dictionary<string, ClassContainer> _classes;
    [SerializeField] private ClassContainer _container;
    [SerializeField] private ClassContainer _container2;
    [SerializeField] public string classChoice;

    private NetworkVariable<FixedString64Bytes> internalClassChoice;

    private void Awake()
    {
        _classes = new Dictionary<string, ClassContainer>();
        _classes.Add("Void", _container);
        _classes.Add("Knight", _container2);
        internalClassChoice = new NetworkVariable<FixedString64Bytes>();
        internalClassChoice.OnValueChanged += ((value, newValue) => classChoice = newValue.ToString());



    }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    public override void OnNetworkSpawn()
    {
        if (internalClassChoice.Value == "")
        {
            internalClassChoice.OnValueChanged += SetClassInternal;
        }
        else
        {
            SetClassInternal(internalClassChoice.Value, internalClassChoice.Value);
        }
        
        if (IsOwner)
        {
            SetClassServerRpc(classChoice);
            Debug.Log(internalClassChoice.Value);
        }
        //SetClassInternal(internalClassChoice.Value, internalClassChoice.Value);
        Debug.Log(internalClassChoice.Value);
        //SetClassInternal(internalClassChoice.Value);
        if (!IsOwner)
        {
            
            return;
        }
        //SetClassServerRpc(internalClassChoice.Value);
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

    private void SetClassInternal( FixedString64Bytes previous,FixedString64Bytes className)
    {
        GameObject classMesh = _classes[className.ToString()].mesh;

        var g = Instantiate(classMesh.transform.gameObject, transform.position, transform.rotation);
        g.transform.parent = transform;
        for (int i = 0; i < g.transform.childCount; i++)
        {
            var child = g.transform.GetChild(i);
            child.transform.parent = transform;
        }
        
        GetComponent<Animator>().Rebind();
        
        Debug.Log("Object name: "+ transform.name, gameObject);
    }

    [ClientRpc]
    public void SendClassNameClientRpc(FixedString64Bytes className)
    {
        SetClassInternal(internalClassChoice.Value, internalClassChoice.Value);
    }
    
    [ServerRpc(RequireOwnership = false)]
    public void SetClassServerRpc(FixedString64Bytes className)
    {
        internalClassChoice.Value = className;
        //SendClassNameClientRpc(className);
        
        
        Debug.Log("Object name: "+ transform.name, gameObject);
        
    }

}
