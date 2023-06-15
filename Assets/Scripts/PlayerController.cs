using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private StateController controller;

    [SerializeField] private State ability1;
    // Start is called before the first frame update
    void Start()
    {
        
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

    private void CastAbility(State ability)
    {
        if (controller.highestPriority<= ability.priority)
        {
            controller.AddState(ability);
        }
       
    }
}
