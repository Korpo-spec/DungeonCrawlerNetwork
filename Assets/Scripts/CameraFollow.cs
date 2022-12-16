using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public Transform objToFollow;

    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (objToFollow)
        {
            transform.position = objToFollow.position + offset;
        }
        
    }
}
