using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Player/ShootGun")]
public class PistolShotState : State
{

    [SerializeField] private MovementState movementstate;
    private Animator animator;
    private static readonly int ShootGun = Animator.StringToHash("ShootGunn");

    public override void OnEnter(StateController controller)
    {
        base.OnEnter(controller);
        animator = controller.GetComponent<Animator>();
        //controller.transform.Rotate(new Vector3(0,90,0));
        animator.SetBool(ShootGun, true);
    }

    public override void UpdateState()
    {
        if (!animator.IsPlayingState("ShootingGun"))
        {
            animator.SetBool(ShootGun, false);
            Debug.Log("transition");
            controller.Transistion(movementstate);
        }

    }
}
