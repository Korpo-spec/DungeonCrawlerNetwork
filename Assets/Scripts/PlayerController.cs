using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private Camera camera;
    private Vector3 mousePos;
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
        if (IsOwner)
        {
            camera = FindObjectOfType<Camera>();
            camera.GetComponentInParent<CameraFollow>().objToFollow = transform;
        }
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
        var rotatedMoveVec = camera.transform.rotation * moveVec;
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

        if (Input.GetMouseButton(1))
        {
            camera.transform.RotateAround(transform.position,Vector3.up,Input.mousePosition.x-mousePos.x);
            camera.transform.RotateAround(transform.position,camera.transform.right,mousePos.y-Input.mousePosition.y);
        }

        

        mousePos = Input.mousePosition;

        
        

        //TranslateServerRpc(transform.position+moveVec* Time.deltaTime);


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
