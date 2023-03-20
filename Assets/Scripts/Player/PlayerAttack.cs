using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public delegate IEnumerator OnPlayerRotationEventHandler(Transform target);
    public static event OnPlayerRotationEventHandler OnPlayerRotation;

   

    private Coroutine enemyLookCoroutine;
    private void OnDisable()
    {
        AttackRadius.OnPlayerAttack -= PlayerRotation;
    }
    private void OnEnable()
    {
        AttackRadius.OnPlayerAttack += PlayerRotation;
    }
    private void PlayerRotation(IDamageable Target)
    {

        if (enemyLookCoroutine != null)
        {
            StopCoroutine(enemyLookCoroutine);
        }

        enemyLookCoroutine = StartCoroutine(OnPlayerRotation?.Invoke(Target.GetTransform()));
      
    }

}
