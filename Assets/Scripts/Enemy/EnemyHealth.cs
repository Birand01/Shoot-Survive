using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthBase
{
  

    /// -------- DELEGATES ----------
    public delegate void OnEnemyDeadAnimationHandler(GameObject gameObject, string name);
    public delegate void OnEnemyRemoveFromListEventHandler(IDamageable damageable);
    public delegate void OnPlayerExperienceTextHandler(GameObject gameObject,float experienceGive);
    public delegate void OnEnemyFloatingTextHandler(GameObject gameObject, float health);
    public delegate void OnGoldGenerateHandler(Transform enemy);
    public delegate void OnPlayerExperienceGainHandler(float expGain);
   


    /// -------- EVENTS --------------
    public static event OnEnemyRemoveFromListEventHandler OnEnemyRemoveFromList;
    public static event OnEnemyDeadAnimationHandler OnEnemyDeadAnimation;
    public static event OnPlayerExperienceTextHandler OnPlayerExperienceText;
    public static event OnEnemyFloatingTextHandler OnEnemyFloatTextAnimation;
    public static event OnGoldGenerateHandler OnGoldGenerate;
    public static event OnPlayerExperienceGainHandler OnPlayerExpGain;
    public event System.Action OnEnemyDead;

    internal float experienceGive,goldGive;
   
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if(Health>0)
        {
            OnEnemyFloatTextAnimation(this.gameObject, Health);
        }
        if (Health <= 0)
        {
            OnEnemyDead?.Invoke();
            OnPlayerExpGain?.Invoke(experienceGive);
            OnGoldGenerate?.Invoke(this.transform);
            OnEnemyRemoveFromList?.Invoke(this.gameObject.GetComponent<IDamageable>());
            OnEnemyDeadAnimation?.Invoke(this.gameObject, "Dead");
            OnPlayerExperienceText?.Invoke(this.gameObject,experienceGive);
            this.gameObject.GetComponent<Collider>().enabled = false;
            StartCoroutine(DisableCoroutine());
        }

       
    }
    private IEnumerator DisableCoroutine()
    {
        this.gameObject.GetComponent<Enemy>().DisableNavmeshAgent();
        yield return new WaitForSeconds(1.3f);
        this.gameObject.SetActive(false);

    }
}
