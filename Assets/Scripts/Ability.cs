using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Ability")]
public class Ability : ScriptableObject
{
    public Sprite sprite;
    public int cost;
    public State abilityCode;

    public void OnAbilityCast(StateController controller)
    {
        controller.AddState(abilityCode);
    }
}
