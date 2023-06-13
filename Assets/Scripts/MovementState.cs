using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Player/Movement")]
public class MovementState : State
{
    [SerializeField] private PistolShotState shootstate;
    private Animator animator;
    private Camera camera;


    private static readonly int Walking = Animator.StringToHash("walking");
    private static readonly int Running = Animator.StringToHash("running");

    public override void OnEnter(StateController controller)
    {
        base.OnEnter(controller);
        GetCamera();
        animator = controller.GetComponent<Animator>();
    }
    public override void UpdateState()
    {
        if (!camera) GetCamera();
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        Vector3 moveVec = new Vector3(inputX, 0, inputY);
        Vector3 rotatedMoveVec = camera.transform.rotation * moveVec;
        rotatedMoveVec.y = 0;
        rotatedMoveVec.Normalize();
        if (moveVec.magnitude > 0)
        {
            animator.SetBool(Walking, true);
            animator.SetBool(Running, Input.GetKey(KeyCode.LeftShift));
            controller.transform.rotation = Quaternion.LookRotation(rotatedMoveVec);
            //transform.Translate(rotatedMoveVec * Time.deltaTime, Space.World);
        }
        else
        {
            animator.SetBool(Running, false);
            animator.SetBool(Walking, false);
        }
        if (Input.GetKeyDown((KeyCode.F)))
        {
            controller.AddState(shootstate);
        }
    }
    private void GetCamera()
    {
        camera = FindObjectOfType<Camera>();
    }
}