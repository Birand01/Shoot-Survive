using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    // DELEGATES
    public delegate void OnWaveInformationText(int counter,bool state);
   
    // EVENTS
   
    public static event OnWaveInformationText OnWaveInformation;


    public Wave[] waves;
    private Wave currentWave;
    int currentWaveNumber;
    int enemiesRemainingToSpawn,enemiesRemainingAlive;
    float nextSpawnTime;
    private float waveCountDown;
    private Transform spawnPointParent;
    [SerializeField] private List<Transform> spawnList;

    //Randomizer
    private double accumulatedWeights;
    private System.Random random = new System.Random();

   
    private void Awake()
    {
        
        spawnPointParent = GameObject.FindGameObjectWithTag("EnemySpawnPoints").transform;
        for (int i = 0; i < spawnPointParent.childCount; i++)
        {
            spawnList.Add(spawnPointParent.GetChild(i));
        }
       // CalculateWeights();
    }
    private void Start()
    {
      
        NextWave();
    }

    private void Update()
    {

        InstantiateEnemy();

    }
    private void InstantiateEnemy()
    {
        
        if (waveCountDown <= 0 )
        {
            OnWaveInformation?.Invoke(currentWaveNumber, false);
            if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
            {
                enemiesRemainingToSpawn--;
                nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;


                
                    EnemyHealth enemy = Instantiate(currentWave.enemy[Random.Range(0, currentWave.enemy.Length)].enemy);
                    enemy.transform.position = spawnPointParent.GetChild(Random.Range(0, spawnList.Count)).transform.position;
                    enemy.transform.rotation = Quaternion.identity;
                    enemy.OnEnemyDead += OnEnemyDead;
                
               
               
            }

        }
        else
        {
            waveCountDown -= Time.deltaTime;

        }
    }
    void OnEnemyDead()
    {
        enemiesRemainingAlive--;
        Debug.Log(enemiesRemainingAlive);
        if(enemiesRemainingAlive==0)
        {
            
            NextWave();
        }
        
            
       
        Debug.Log("DIED");
    }
    void NextWave()
    {
       
        currentWaveNumber++;
       
        OnWaveInformation?.Invoke(currentWaveNumber, true);
        if (currentWaveNumber-1<waves.Length)
        {
            
            currentWave = waves[currentWaveNumber - 1];
            enemiesRemainingToSpawn = currentWave.numberToSpawn;
            enemiesRemainingAlive = enemiesRemainingToSpawn;
            waveCountDown = currentWave.timeBeforeThisWave;
        }
      

    }

    private int GetRandomEnemyIndex()
    {
        double r = random.NextDouble() * accumulatedWeights;
        for (int i = 0; i < currentWave.enemy.Length; i++)
        {
            if (currentWave.enemy[i]._weight>=r)
            {
                return i;
            }
        }
        return 0;
    }
    private void CalculateWeights()
    {
        accumulatedWeights = 0f;
        foreach (EnemyWeight enemy in currentWave.enemy )
        {
            accumulatedWeights += enemy.chance;
            enemy._weight = accumulatedWeights;
            Debug.Log("Accumulated weight is :" +accumulatedWeights);
        }
    }
    
}   
  




