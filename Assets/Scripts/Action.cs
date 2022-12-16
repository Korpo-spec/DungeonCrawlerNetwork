using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    public virtual void OnEnterAction(State state)
    {
        
    }

    public virtual void OnUpdateAction()
    {
        
    }

    public virtual void OnExitAction()
    {
        
    }
    
    public void OnTriggerEnter(Collider other)
    {
        
    }
}
