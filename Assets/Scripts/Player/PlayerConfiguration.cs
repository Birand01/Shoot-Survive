using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfiguration : MonoBehaviour
{
    [SerializeField] internal PlayerScriptableObject playerConfiguration;
    public delegate void OnPlayerMaxHealthTextHandler(float maxHealth);
    public static event OnPlayerMaxHealthTextHandler OnPlayerMaxHealthText;
    public delegate void OnPlayerCurrentHealthTextHandler(float maxHealth);
    public static event OnPlayerCurrentHealthTextHandler OnPlayerCurrentHealthText;


    private void OnEnable()
    {
        playerConfiguration.SetUpPlayerFromConfiguration(this);
       
    }
    private void Start()
    {
        SetMaxHealthUI(playerConfiguration.maxHealth);
        
    }
    private void Update()
    {
        SetCurrentHealth(this.gameObject.GetComponent<PlayerHealth>().Health);
    }


    private void SetMaxHealthUI(float maxHealth)
    {
       maxHealth=playerConfiguration.maxHealth;
        OnPlayerMaxHealthText?.Invoke(maxHealth);
    }
    
    private void SetCurrentHealth(float currentHealth)
    {      
        OnPlayerCurrentHealthText?.Invoke(currentHealth);
    }

}
