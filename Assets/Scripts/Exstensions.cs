using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Exstensions 
{
    public static bool IsPlayingState(this Animator animator, string animStateName)
    {
        return !(!animator.GetCurrentAnimatorStateInfo(0).IsName(animStateName) && !animator.IsInTransition(0));
    }

    public static Vector3 RoundVector3(this Vector3 vec, int decimals)
    {
        Debug.Log(vec.x);
        vec.x = MathF.Round(vec.x, decimals);
        Debug.Log(vec.x);
        vec.y = MathF.Round(vec.y, decimals);
        vec.z = MathF.Round(vec.z, decimals);
        return vec;
    }

    public static Transform FindRecusiveChild(this Transform transform, string name)
    {
        foreach(Transform child in transform)
        {
            if(child.name == name ) return child;
            var result = child.FindRecusiveChild(name);
            if (result != null)
                return result;
        }
        return null;
        
    }
    
    
}
