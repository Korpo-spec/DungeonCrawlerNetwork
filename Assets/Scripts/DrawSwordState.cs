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
    
    private Vector3 Handpos = Vector3.zero;
    private Vector3 Handrot = Vector3.zero;
    
    private NetworkAnimator networkAnimator;
    /*[SerializeField] private GameObject Spine;
    [SerializeField] private Transform SpinePos;*/
    
    
    

    
    public override void OnEnter(StateController controller)
    {
        base.OnEnter(controller);
        Sword = controller.transform.FindRecusiveChild("SM_Item_Sword");
        Sword.transform.position = Handpos;
        Sword.transform.rotation = Quaternion.Euler(Handrot);
        Hand = controller.transform.FindRecusiveChild("Hand_R");
        networkAnimator = controller.GetComponent<NetworkAnimator>();
        networkAnimator.SetTrigger("DrawSword");
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
    }
}
