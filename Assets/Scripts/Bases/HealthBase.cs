using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class HealthBase : MonoBehaviour, IDamageable
{
    public delegate void OnPlayerCurrentHealthTextHandler(float health);
    public static event OnPlayerCurrentHealthTextHandler OnPlayerCurrentHealthText;

    protected float health;
    [SerializeField] public Slider healthBarSlider;
    [SerializeField] protected Image healthBarFillImage;
    [SerializeField] protected Color maxHealthColor, minHealthColor;

    public float Health
    {
        get { return health; }
        set { health = value; }
    }
    

    protected virtual void Update()
    {
        SetHealthBarUI();
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
        Health = Mathf.Clamp(Health, 0, healthBarSlider.value);
        OnPlayerCurrentHealthText?.Invoke(Health);
        SetHealthBarUI();
    }
    private void SetHealthBarUI()
    {
        float healthPercentage = CalculateHealthPercentage();
        healthBarSlider.value = healthPercentage;
        healthBarFillImage.color = Color.Lerp(minHealthColor, maxHealthColor, healthPercentage / healthBarSlider.maxValue);


    }
    private float CalculateHealthPercentage()
    {
        return (Health / healthBarSlider.maxValue) * healthBarSlider.maxValue;
    }
}
