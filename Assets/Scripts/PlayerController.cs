using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    // Start is called before the first frame update

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        transform.TranslateServerRpc(new Vector3(inputX, 0, inputY)* Time.deltaTime);
    }
    
    
}

public static class Extensions
{
    [ServerRpc]
    public static void TranslateServerRpc(this Transform transform, Vector3 movVec)
    {
        transform.Translate(movVec);
    }
    
    

}
