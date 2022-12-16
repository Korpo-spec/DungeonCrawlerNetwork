using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : ScriptableObject
{
    public StateController controller;

    [NonSerialized] private bool instatiated;
    
    public virtual void OnEnter(StateController controller)
    {
        this.controller = controller;
    }

    public virtual void UpdateState()
    {
        
    }

    public virtual void OnExit()
    {
        
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        
    }
}
