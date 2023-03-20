using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBase : MonoBehaviour
{
    public delegate void OnGoldTextCounterHandler(int counter);
    public static event OnGoldTextCounterHandler OnGoldTextCounter;
    public delegate void OnExpTextCounterHandler(float counter);
    public static event OnExpTextCounterHandler OnExpTextCounter;

     private int totalGoldAmont;
     private float totalExpAmount;
    [SerializeField] private float decreaseExpAmount;
    private void OnEnable()
    {
        ButtonManager.OnHealthButtonPressed += DecraseTotalExpAmount;
        GoldInteraction.OnGoldCounter += TotalGold;
        EnemyHealth.OnPlayerExpGain += TotalExp;
       
    }
    private void TotalGold(int goldToAdd)
    {
        totalGoldAmont += goldToAdd;
        OnGoldTextCounter?.Invoke(totalGoldAmont);
        
    }
    public void DecraseTotalExpAmount(float amount)
    {
        if(decreaseExpAmount < totalExpAmount)
        {
            amount = decreaseExpAmount;
            totalExpAmount -= amount;
        }

        
    }
    private void TotalExp(float expToAdd)
    {
        totalExpAmount += expToAdd;
        OnExpTextCounter?.Invoke(totalExpAmount);
    }
    private void OnDisable()
    {
        ButtonManager.OnHealthButtonPressed -= DecraseTotalExpAmount;
        EnemyHealth.OnPlayerExpGain -= TotalExp;
        GoldInteraction.OnGoldCounter -= TotalGold;
    }
}
