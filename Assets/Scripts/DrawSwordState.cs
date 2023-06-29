using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Components;
using UnityEngine;
[CreateAssetMenu(menuName = "State/Player/DrawSword")]
public class DrawSwordState : State
{
    [SerializeField] private MovementState movementstate;

    private Transform Sword;
    private Transform Hand;
    
    private Vector3 Handpos = new Vector3(-3.9f,-20.4f,-42.6f);
    private Vector3 Handrot = new Vector3(70.1f, 33.27f, 30.84f);
    
    private NetworkAnimator networkAnimator;
    
    
    /*[SerializeField] private GameObject Spine;
    [SerializeField] private Transform SpinePos;*/
    
    
    

    
    public override void OnEnter(StateController controller)
    {
        base.OnEnter(controller);
        Sword = controller.transform.FindRecusiveChild("SM_Item_Sword");
        Hand = controller.transform.FindRecusiveChild("Hand_R");
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
        Sword.SetParent(Hand,false);
        Sword.transform.localPosition = Handpos;
        Sword.transform.localRotation = Quaternion.Euler(Handrot);
        
        networkAnimator.Animator.applyRootMotion = true;
        
    }
}
