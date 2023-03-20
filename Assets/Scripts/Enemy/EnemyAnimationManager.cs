using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    private Animator anim;
    private void OnEnable()
    {
        Enemy.OnEnemyAttackAnimation += AnimationTrigger;
        EnemyHealth.OnEnemyDeadAnimation+=AnimationTrigger;
        DamageDetection.OnEnemyIdleAnimation+=AnimationTrigger;
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void AnimationState(GameObject gameObject, string name, bool state)
    {
        gameObject.GetComponent<Animator>().SetBool(name, state);
    }
    private void AnimationTrigger(GameObject gameObject, string name)
    {
        gameObject.GetComponent<Animator>().SetTrigger(name);
    }
    private void OnDisable()
    {
        DamageDetection.OnEnemyIdleAnimation -= AnimationTrigger;
        EnemyHealth.OnEnemyDeadAnimation -= AnimationTrigger;
        Enemy.OnEnemyAttackAnimation -= AnimationTrigger;
    }
}
