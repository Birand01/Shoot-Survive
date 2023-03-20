using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectableManager : MonoBehaviour
{
    public delegate void OnGoldCollectHandler(Vector3 gameObject);
    public static event OnGoldCollectHandler OnGoldCollect;

    [SerializeField] private GameObject goldPrefab;
    private Transform player;
    private void OnEnable()
    {
        EnemyHealth.OnGoldGenerate += GenerateGold;
    }
   
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void GenerateGold(Transform enemyPosition)
    {
        GameObject gold = Instantiate(goldPrefab);
        gold.transform.position = enemyPosition.position;
        gold.transform.rotation = enemyPosition.rotation;
        OnGoldCollect?.Invoke(player.position);
    }

    
    
    private void OnDisable()
    {
        EnemyHealth.OnGoldGenerate -= GenerateGold;
    }
}
