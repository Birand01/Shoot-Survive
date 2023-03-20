using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DamageDetection : InteractionBase
{
   
    public delegate void OnPlayerDecreaseHealthHandler(float value);
    public static event OnPlayerDecreaseHealthHandler OnPlayerDecreaseHealth;
    public delegate void OnEnemyIdleAnimationHandler(GameObject gameObject, string name);
    public static event OnEnemyIdleAnimationHandler OnEnemyIdleAnimation;

    public delegate void OnEnemyAttackPlayerHandler();
    public static event OnEnemyAttackPlayerHandler OnEnemyAttackPlayer;
   
   
    protected override void OnTriggerStayAction(Collider other)
    {
        Enemy skeleton = other.GetComponent<Enemy>();
        if (skeleton != null)
        {
            OnPlayerDecreaseHealth?.Invoke(other.gameObject.GetComponent<Enemy>().enemyDamage);
        }
        
        else
            return;
       
    }

    protected override void OnTriggerExitAction(Collider other)
    {
        base.OnTriggerExitAction(other);
        //OnEnemyIdleAnimation?.Invoke(this.gameObject, "Idle");
    }


    protected override void OnTriggerEnterAction(Collider collider)
    {
        OnEnemyAttackPlayer?.Invoke();
        SpawnParticle(_particlePrefab);
       //SpawnAudio(_pickupSFX);
    }
}
    