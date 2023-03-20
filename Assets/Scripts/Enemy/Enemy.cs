using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase
{
    public delegate void OnEnemyAttackAnimationHandler(GameObject gameObject, string name);
    public static event OnEnemyAttackAnimationHandler OnEnemyAttackAnimation;

  

    public delegate void OnEnemyDamageToPlayerHandler(float damage);
    public static event OnEnemyDamageToPlayerHandler OnEnemyDamageToPlayer;
    [SerializeField] private float delay;
    internal float enemyDamage;
    [SerializeField] private EnemyScriptableObject enemyScriptableObject;

   
    private void OnEnable()
    {
        DamageDetection.OnEnemyAttackPlayer += AttackToPlayer;
    }
    private void OnDisable()
    {
        DamageDetection.OnEnemyAttackPlayer -= AttackToPlayer;
    }
    private void Start()
    {
        enemyScriptableObject.SetUpAgentFromConfiguration(this);
        StartChasing();
       
    }
    private void Update()
    {
        //Attack();
       
    }

    private void AttackToPlayer()
    {
        StartCoroutine(AttackCoroutine());
    }
   
    private IEnumerator AttackCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(delay);
        while (agent.enabled == true && destination.gameObject.activeInHierarchy)
        {
            Attack();
            yield return wait;
        }


    }
    private void Attack()
    {
        
            if (IsEnemyInAttackRange() )
            {
                OnEnemyDamageToPlayer?.Invoke(enemyDamage);
                agent.isStopped = true;
                agent.enabled = false;
                OnEnemyAttackAnimation?.Invoke(this.gameObject, "Attack");

            }
       
    }

   
}
