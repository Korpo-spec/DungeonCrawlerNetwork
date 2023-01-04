using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Player/Movement")]
public class MovementState : State
{
    public float MaxZoom = 10;
    public float MinZoom = 0.1f;
    [SerializeField] private PistolShotState shootstate;
    private Animator animator;
    private Camera camera;

    private Vector3 mousePos;
    private Vector2 zoomLevel;


    private static readonly int Walking = Animator.StringToHash("walking");
    private static readonly int Running = Animator.StringToHash("running");

    public override void OnEnter(StateController controller)
    {
        base.OnEnter(controller);
        camera = FindObjectOfType<Camera>();
        animator = controller.GetComponent<Animator>();
        camera.GetComponentInParent<CameraFollow>().objToFollow = controller.transform;
    }

    public override void UpdateState()
    {
        
        Vector3 cameraForward = camera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();
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

        ManageCameraRotation(Input.GetMouseButton(1));
        ManageCameraZoom(Input.mouseScrollDelta);

        mousePos = Input.mousePosition;
        zoomLevel = Input.mouseScrollDelta;

        if (Input.GetKeyDown((KeyCode.F)))
        {
            controller.Transistion(shootstate);
        }
    }

    private void ManageCameraRotation(bool getMouseButton)
    {
        if (getMouseButton)
        {
            camera.transform.RotateAround(controller.transform.position, Vector3.up,
                Input.mousePosition.x - mousePos.x);
            camera.transform.RotateAround(controller.transform.position, camera.transform.right,
                mousePos.y - Input.mousePosition.y);
        }
    }

    private void ManageCameraZoom(Vector2 mouseScrollDelta)
    {
        if (mouseScrollDelta.magnitude < 0.1)
        {
            return;
        }

        //var oldDistanceToObj = camera.GetComponentInParent<CameraFollow>().DistanceToObj;
        var oldDistanceToObj = Vector3.Distance(camera.transform.position, controller.transform.position);
        var newDistanceToObj = oldDistanceToObj;
        newDistanceToObj += mouseScrollDelta.y;
        if (newDistanceToObj < MinZoom)
        {
            newDistanceToObj = MinZoom;
        }

        if (newDistanceToObj > MaxZoom)
        {
            newDistanceToObj = MaxZoom;
        }

        Vector3 newPosition = camera.transform.localPosition;
        newPosition *= newDistanceToObj / oldDistanceToObj;
        camera.transform.localPosition = newPosition;
    }
}