using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldInteraction : InteractionBase
{
    [SerializeField] private int goldToAdd;
    public delegate void OnGoldCounterHandler(int goldValue);
    public static event OnGoldCounterHandler OnGoldCounter;

    protected override void OnTriggerEnterAction(Collider collider)
    {
       
        OnGoldCounter?.Invoke(goldToAdd);
        this.gameObject.GetComponent<Collider>().enabled = false;
    }

   
}
