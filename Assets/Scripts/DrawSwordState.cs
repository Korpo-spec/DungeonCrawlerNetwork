using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
[CreateAssetMenu(menuName = "State/Player/DrawSword")]
public class DrawSwordState : State
{
    [SerializeField] private MovementState movementstate;

    
    
    private Vector3 Handpos = new Vector3(-3.9f,-20.4f,-42.6f);
    private Vector3 Handrot = new Vector3(70.1f, 33.27f, 30.84f);
    
    private NetworkAnimator networkAnimator;
    
    
    /*[SerializeField] private GameObject Spine;
    [SerializeField] private Transform SpinePos;*/
    
    
    
    
    
    public override void OnEnter(StateController controller)
    {
        base.OnEnter(controller);
        
        networkAnimator = controller.GetComponent<NetworkAnimator>();
        networkAnimator.Animator.applyRootMotion = false;
        networkAnimator.SetTrigger("DrawSword");
        controller.ClearCurrentStates(this);
    }

    public override void UpdateState()
    {
        
    }

    public override void OnAnimatorEvent(string eventName)
    {
        if (eventName != "EquipSword")
        {
            return;
        }
        controller.ChangeParentServerRpc("SM_Item_Sword", "Hand_R", Handpos, Quaternion.Euler(Handrot));
        
    }

    
    
    
}
