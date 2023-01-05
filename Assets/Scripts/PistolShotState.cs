using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Player/ShootGun")]
public class PistolShotState : State
{

    [SerializeField] private MovementState movementstate;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject rangeObj;
    private NetworkAnimator networkAnimator;
    private bool hasShot;
    private bool aiming;
    
    
    private static readonly int ShootGun = Animator.StringToHash("ShootGun");

    public override void OnEnter(StateController controller)
    {
        base.OnEnter(controller);
        networkAnimator = controller.GetComponent<NetworkAnimator>();
        aiming = true;
        //controller.transform.Rotate(new Vector3(0,90,0));
        
        rangeObj = Instantiate(rangeObj, controller.transform.position, rangeObj.transform.rotation);
    }

    public override void UpdateState()
    {
        //rangeObj.transform.rotation = Quaternion.Euler(Camera.main.ScreenToWorldPoint(Input.mousePosition) -controller.transform.position);
        if (aiming)
        {
            Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,10));
            vec.y = controller.transform.position.y;
            vec -= controller.transform.position;
            rangeObj.transform.rotation = Quaternion.LookRotation(vec, Vector3.up);
            if (Input.GetMouseButtonDown(0))
            {
                aiming = false;
                Destroy(rangeObj);
                networkAnimator.SetTrigger(ShootGun);
            }
            else
            {
                return;
            }
            
            
        }
        
        
        
        
        
        
        if (!hasShot)
        {
            ProjectileSpawnData customData = new ProjectileSpawnData();
            Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,10));
            vec.y = controller.transform.position.y;
            vec -= controller.transform.position;
            
            customData.direction = vec;
            customData.direction.y = 0;
            customData.direction.Normalize();
            
            controller.SpawnServerRpc(projectile.name, controller.transform.position, Quaternion.identity, JsonUtility.ToJson(customData));
            hasShot = true;
        }
        
        
        
        if (!networkAnimator.Animator.IsPlayingState("ShootingGun"))
        {
            
            Debug.Log("transition");
            controller.Transistion(movementstate);
        }

    }

    public override void OnGetSpawnedGameObj(string nameOfGameObj)
    {
        base.OnGetSpawnedGameObj(nameOfGameObj);
    }
}
