using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class StateController : NetworkBehaviour
{
    [SerializeField] private State defaultState;

    private State currentState;

    private State nextState;

    private bool changeState;

    private bool ignoreFirstUpdate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            enabled = false;
            return;
        }
        currentState = Instantiate(defaultState);
        currentState.OnEnter(this);
    }

    // Update is called once per frame
    void Update()
    {

        if (ignoreFirstUpdate)
        {
            ignoreFirstUpdate = false;
            return;
        }
        
        currentState.UpdateState();
        if (changeState)
        {
            OnTransition();
            return;
        }
    }

    public void Transistion(State newstate)
    {
        nextState = newstate;
        changeState = true;
    }

    private void OnTransition()
    {
        changeState = false;
        currentState.OnExit();
        currentState = Instantiate(nextState);
        currentState.OnEnter(this);
        ignoreFirstUpdate = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }
}
