using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : InteractionBase
{
    public delegate void OnPlayerInteractionEvent(Collider collider);
    public static event OnPlayerInteractionEvent OnEnemyAddToList;


    protected override void OnTriggerStayAction(Collider other)
    {
        base.OnTriggerStayAction(other);

    }
    protected override void OnTriggerEnterAction(Collider collider)
    {
        OnEnemyAddToList?.Invoke(collider);
       
    }
}
