using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Wave",menuName ="ScriptableObjects/Waves",order =1)]
public class Wave : ScriptableObject
{
    public EnemyWeight[] enemy;
    public float timeBeforeThisWave;
    public int numberToSpawn;
    public float timeBetweenSpawns;
   

}
