using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        Vector3 moveVec = new Vector3(inputX, 0, inputY);
        if (moveVec.magnitude > 0)
        {
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
        }
        
        transform.Translate(moveVec* Time.deltaTime);
        TranslateServerRpc(transform.position+moveVec* Time.deltaTime);
        
        
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
