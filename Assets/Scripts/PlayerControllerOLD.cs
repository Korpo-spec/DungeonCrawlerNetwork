using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

/*
 *  OBS:OBSOLETE CODE
 * Code has been moved to MovementState.cs and is only a reference for future bugs
 * 
 */
public class PlayerControllerOLD : NetworkBehaviour
{
    public float MaxZoom = 10;
    public float MinZoom = 0.1f;
    // Start is called before the first frame update
    private Animator animator;
    private Camera camera;
    private Vector3 mousePos;
    private Vector2 zoomLevel;
    
    private static readonly int Walking = Animator.StringToHash("walking");
    private static readonly int Running = Animator.StringToHash("running");

    void Start()
    {
        //mousePos = Input.mousePosition;
        camera = FindObjectOfType<Camera>();
        animator = GetComponent<Animator>();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (!IsOwner) return;
        camera = FindObjectOfType<Camera>();
        camera.GetComponentInParent<CameraFollow>().objToFollow = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
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
            transform.rotation = Quaternion.LookRotation(rotatedMoveVec);
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
        //TranslateServerRpc(transform.position+moveVec* Time.deltaTime);
    }

    private void ManageCameraRotation(bool getMouseButton)
    {
        if (getMouseButton)
        {
            camera.transform.RotateAround(transform.position,Vector3.up,Input.mousePosition.x-mousePos.x);
            camera.transform.RotateAround(transform.position,camera.transform.right,mousePos.y-Input.mousePosition.y);
        }
        
    }

    private void ManageCameraZoom(Vector2 mouseScrollDelta)
    {
        if (mouseScrollDelta.magnitude < 0.1)
        {
            return;
        }
        //var oldDistanceToObj = camera.GetComponentInParent<CameraFollow>().DistanceToObj;
        var oldDistanceToObj = Vector3.Distance(camera.transform.position, transform.position);
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
        newPosition *= newDistanceToObj/oldDistanceToObj;
        camera.transform.localPosition = newPosition;
    }
    
    [ServerRpc]
    public void TranslateServerRpc(Vector3 movVec)
    {
        transform.position = movVec;
    }
    
}

public static class Extensions
{
    
    
    

}
