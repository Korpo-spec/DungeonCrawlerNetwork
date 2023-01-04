using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Exstensions 
{
    public static bool IsPlayingState(this Animator animator, string animStateName)
    {
        return !(!animator.GetCurrentAnimatorStateInfo(0).IsName(animStateName) && !animator.IsInTransition(0));
    }
}
