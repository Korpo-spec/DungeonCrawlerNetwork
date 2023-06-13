using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class StateController : NetworkBehaviour
{
    [SerializeField] private State defaultState;

    private List<State> currentState;

    private Queue<(State state, bool hardshift)> nextState;

    private bool changeState;

    private bool ignoreFirstUpdate;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        currentState = new List<State>();
        nextState = new Queue<(State state, bool hardshift)>();
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            enabled = false;
            return;
        }
        currentState.Add( Instantiate(defaultState));
        currentState[0].OnEnter(this);
    }
    

    // Update is called once per frame
    void Update()
    {

        if (!IsOwner) return;
        
        if (ignoreFirstUpdate)
        {
            ignoreFirstUpdate = false;
            return;
        }

        foreach (var state in currentState)
        {
            state.UpdateState();
        }
        
        if (changeState)
        {
            for (int i = 0; i < nextState.Count; i++)
            {
                var nextstate = nextState.Dequeue();
                if (nextstate.hardshift)
                {
                    OnTransition(nextstate.state);
                }
                else
                {
                    OnAdd(nextstate.state);
                }
                
            }
            
            return;
        }
    }

    public void Transistion(State newstate)
    {
        nextState.Enqueue((newstate, true)); 
        changeState = true;
    }

    public void AddState(State newstate)
    {
        nextState.Enqueue((newstate, false)); 
        changeState = true;
    }

    private void OnTransition(State changestate)
    {
        RemoveActiveStates();
        changeState = false;
        
        State newstate = Instantiate(changestate);
        currentState.Add( newstate);
        newstate.OnEnter(this);
        ignoreFirstUpdate = true;
    }


    public void ClearCurrentStates()
    {
        RemoveActiveStates();
    }
    public void ClearCurrentStates(State exception)
    {
        RemoveActiveStates(exception);
    }
    private void RemoveActiveStates()
    {
        foreach (var state in currentState)
        {
            state.OnExit();
        }
        currentState.Clear();
    }
    
    private void RemoveActiveStates(State exception)
    {
        foreach (var state in currentState)
        {
            if (state == exception)
            {
                continue;
            }
            state.OnExit();
        }
        currentState.Clear();
        currentState.Add(exception);
    }

    private void OnAdd(State changestate)
    {
        changeState = false;
        
        State newstate = Instantiate(changestate);
        currentState.Add( newstate);
        newstate.OnEnter(this);
        ignoreFirstUpdate = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        foreach (var state in currentState)
        {
            state.OnTriggerEnter(other);
        }
        
    }

    public void AnimationEvent(string eventName)
    {
        if (IsOwner)
        {
            foreach (var state in currentState)
            {
                state.OnAnimatorEvent(eventName);
            }
        }
        
    }
    

    private uint id;
    
    [ServerRpc]
    public void SpawnServerRpc(FixedString128Bytes prefabName, Vector3 position, Quaternion rotation)
    {
        GameObject g = Resources.Load<GameObject>("Prefab/"+ prefabName);
        g = Instantiate(g, position, rotation);
        g.name = prefabName + id.ToString();
        id++;
        g.GetComponent<NetworkObject>().Spawn();
        ClientGetGameObjectClientRpc(g.name);
    }
    
    [ServerRpc]
    public void SpawnServerRpc(FixedString128Bytes prefabName, Vector3 position, Quaternion rotation, FixedString512Bytes jsonData)
    {
        GameObject g = Resources.Load<GameObject>("Prefab/"+ prefabName);
        g = Instantiate(g, position, rotation);
        if (g.TryGetComponent(out ISpawnData data) ) data.DeserializeSpawnData(jsonData.ToString());
        
        g.GetComponent<NetworkObject>().Spawn();
        
    }

    [ClientRpc]
    private void ClientGetGameObjectClientRpc(FixedString128Bytes gameObjectName)
    {
        if (IsOwner)
        {
            //currentState.OnGetSpawnedGameObj(gameObjectName.ToString());
        }
    }
    
    
}
