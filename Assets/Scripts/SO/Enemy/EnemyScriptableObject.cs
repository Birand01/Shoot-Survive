using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyConfiguration", menuName = "ScriptableObjects/EnemyConfiguration",order =2)]
public class EnemyScriptableObject : ScriptableObject
{
    public float health;
    public float movementSpeed;
    public int goldGive;
    public float experienceGive;
    public float updateAIInterval;
    public float enemyDamage;
    public float attackRange;
    public float attackDelay;
    public void SetUpAgentFromConfiguration(EnemyBase enemy)
    {
        enemy.agent.stoppingDistance = attackRange;
        enemy.agent.speed = movementSpeed;
        enemy.updateRate = updateAIInterval;
        enemy.gameObject.GetComponent<EnemyHealth>().experienceGive = experienceGive;
        enemy.gameObject.GetComponent<EnemyHealth>().goldGive =goldGive;
        enemy.gameObject.GetComponent<Enemy>().enemyDamage = enemyDamage;
        enemy.gameObject.GetComponent<EnemyHealth>().Health = health ;
        enemy.gameObject.GetComponent<EnemyHealth>().healthBarSlider.maxValue = health;
        enemy.gameObject.GetComponent<EnemyHealth>().healthBarSlider.value = enemy.gameObject.GetComponent<EnemyHealth>().Health;
    }
}
