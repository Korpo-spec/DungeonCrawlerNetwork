using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Player/ShootGun")]
public class PistolShotState : State
{

    [SerializeField] private MovementState movementstate;
    private Animator animator;
    private static readonly int ShootGun = Animator.StringToHash("ShootGun");

    public override void OnEnter(StateController controller)
    {
        base.OnEnter(controller);
        animator = controller.GetComponent<Animator>();
        //controller.transform.Rotate(new Vector3(0,90,0));
        animator.SetTrigger(ShootGun);
    }

    public override void UpdateState()
    {
        if (animator.IsPlaying("ShootingGun"))
        {
            controller.Transistion(movementstate);
        }

    }
}
