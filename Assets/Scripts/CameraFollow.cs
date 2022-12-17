using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public Transform objToFollow;
    [SerializeField] public Camera cameraRef;

    //public float DistanceToObj;

    // Start is called before the first frame update
    void Start()
    {
        cameraRef = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!objToFollow) return;
        //DistanceToObj = Vector3.Distance(objToFollow.position, cameraRef.transform.position);
        transform.position = objToFollow.position;
        //transform.rotation = objToFollow.rotation;
    }
}
