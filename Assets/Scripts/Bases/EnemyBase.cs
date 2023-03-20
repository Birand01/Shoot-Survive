using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    internal NavMeshAgent agent;  
    internal float updateRate = 0.1f;
    private Coroutine followCoroutine;
    protected Transform destination;

    
    protected virtual void Awake()
    {
        destination = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();

    }
   

    protected virtual void StartChasing()
    {
       
            if (followCoroutine == null)
            {
                followCoroutine = StartCoroutine(FollowTarget());
            }
            else
            {
                Debug.LogWarning("Called startChasing on Enemy is already chasing!");
            }

    }
    private IEnumerator FollowTarget()
    {     

        agent.SetDestination(destination.transform.position);
        WaitForSeconds wait = new WaitForSeconds(updateRate);

        while (enabled && agent.enabled == true)
        {

            if (agent.enabled == false)
            {
                break;
            }
            yield return wait;

        }


    }

    internal bool IsEnemyInAttackRange()
    {
        if (agent.enabled == false)
        {
            return false;
        }
        else
        {
            return agent.remainingDistance <= agent.stoppingDistance;
        }

    }
    protected virtual float DistanceToTarget(Transform target, Transform source)
    {
        //Vector3 distance = target.position - source.position;
        //return distance.sqrMagnitude;
        return Vector3.Distance(target.position, source.position);

    }
    public virtual void DisableNavmeshAgent()
    {
        agent.enabled = false;
       
    }

}
