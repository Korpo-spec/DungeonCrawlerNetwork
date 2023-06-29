using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Ability")]
public class Ability : ScriptableObject
{
    public Sprite sprite;
    public int cost;
    public bool hardShift;
    public State abilityCode;

    public void OnAbilityCast(StateController controller)
    {
        if (hardShift)
        {
            controller.Transistion(abilityCode);
        }
        else
        {
            controller.AddState(abilityCode);
        }
        
    }
}
