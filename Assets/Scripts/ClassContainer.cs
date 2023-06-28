using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ClassContainer : ScriptableObject
{
    [SerializeField] public GameObject mesh;
    [SerializeField] public List<Ability> abilities;
}
