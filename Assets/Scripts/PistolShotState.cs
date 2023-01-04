using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Player/ShootGun")]
public class PistolShotState : State
{

    [SerializeField] private MovementState movementstate;
    [SerializeField] private GameObject projectile;
    private NetworkAnimator networkAnimator;
    
    
    private static readonly int ShootGun = Animator.StringToHash("ShootGun");

    public override void OnEnter(StateController controller)
    {
        base.OnEnter(controller);
        networkAnimator = controller.GetComponent<NetworkAnimator>();
        //controller.transform.Rotate(new Vector3(0,90,0));
        networkAnimator.SetTrigger(ShootGun);
    }

    public override void UpdateState()
    {
        
        
        if (!networkAnimator.Animator.IsPlayingState("ShootingGun"))
        {
            controller.SpawnServerRpc(projectile.name, controller.transform.position);
            
            Debug.Log("transition");
            controller.Transistion(movementstate);
        }

    }

    
}