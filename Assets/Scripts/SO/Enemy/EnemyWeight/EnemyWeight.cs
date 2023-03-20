using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy", order = 4)]
public class EnemyWeight : ScriptableObject
{
    public EnemyHealth enemy;
    [Range(0f, 100f)] public float chance;
    [HideInInspector] public double _weight;
}
