using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResourceable
{
    int MaxResource { get; set; }
    int CurrentResource { get; }
    bool TryCastAbility(int currentResource, Ability abilityToCast);
}
