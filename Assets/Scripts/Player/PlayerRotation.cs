using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
  
    internal float rotationSpeed;
    private void OnEnable()
    {

        PlayerAttack.OnPlayerRotation += LookAt;
        
    }
    private IEnumerator LookAt(Transform target)
    {
        Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);
        float time = 0;
        while (time < 1)
        {
            //lookRotation.x = 0f;
            lookRotation.z = 0f;
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            time += Time.deltaTime * rotationSpeed;
            yield return null;
        }

        transform.rotation = lookRotation;
        
    }
    private void OnDisable()
    {
        PlayerAttack.OnPlayerRotation -= LookAt;
    }
}
