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
    // Start is called before the first frame update
    void Start()
    {
        currentState = Instantiate(defaultState);
        currentState.OnEnter(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
        if (changeState)
        {
            OnTransition();
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
        currentState = nextState;
        currentState.OnEnter(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }
}
