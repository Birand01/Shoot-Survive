using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Networking.Match;

public class AttackRadius : MonoBehaviour
{
    public delegate void OnPlayerAttackEventHandler(IDamageable target);
    public static event OnPlayerAttackEventHandler OnPlayerAttack;


    [SerializeField] private List<IDamageable> damageables;
    internal float minDistanceToAttack;
    internal float attackDelay;
    private Coroutine attackCoroutine;
  

    private void Awake()
    {
       
        damageables = new List<IDamageable>();
     
    }
    private void OnEnable()
    {
        EnemyHealth.OnEnemyRemoveFromList += DeadEnemyEvent;
        PlayerInteraction.OnEnemyAddToList += TriggerEnterEvent;
    }

    
    private void TriggerEnterEvent(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageables.Add(damageable);
            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(Attack());
            }
        }


    }
    private void DeadEnemyEvent(IDamageable damageable) 
    {
        if (damageable != null)
        {

            damageables.Remove(damageable);
            if (damageables.Count == 0)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
    }

    private IEnumerator Attack()
    {
        WaitForSeconds wait = new WaitForSeconds(attackDelay);
        IDamageable closestDamageable = null;
        float closestDistance = minDistanceToAttack;
        while(damageables.Count > 0)
        {
            for (int i = 0; i < damageables.Count; i++)
            {
                Transform damageableTransform = damageables[i].GetTransform();
                float distanceToTarget = Vector3.Distance(transform.position, damageableTransform.position);
                if(distanceToTarget<closestDistance)
                {
                    closestDistance = distanceToTarget;
                    closestDamageable = damageables[i];
                    
                }
            }

            if(closestDamageable != null)
            {
                OnPlayerAttack?.Invoke(closestDamageable);
            }

            closestDamageable = null;
            closestDistance = minDistanceToAttack;
            yield return wait;
            damageables.RemoveAll(DisabledDamageables);

        }
        attackCoroutine = null;
    }
    private bool DisabledDamageables(IDamageable damageable)
    {
        return damageable != null && !damageable.GetTransform().gameObject.activeSelf;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyRemoveFromList -= DeadEnemyEvent;
        PlayerInteraction.OnEnemyAddToList -= TriggerEnterEvent;
    }
}
