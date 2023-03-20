using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : HealthBase
{

    // -------- DELEGATES ----------
    public delegate void OnPlayerDeadExplosionSoundHandler(AudioClip deadSound,float volume);
    public delegate void OnLevelFailEventHandler(bool state);
    // -------- EVENTS ------------
    public static event OnPlayerDeadExplosionSoundHandler OnPlayerDeadExplosionSound;
    public static event OnLevelFailEventHandler OnLevelFail;


    private Collider[] colliders;
    internal AudioClip deadSound;
    internal float volume;
    [SerializeField] private ParticleSystem deadParticle;
   
    private void OnEnable()
    {

        ButtonManager.OnHealthButtonPressed += IncreaseHealth;
        DamageDetection.OnPlayerDecreaseHealth += TakeDamage;
        healthBarSlider = GameObject.FindGameObjectWithTag("HealthSlider").GetComponent<Slider>();
        healthBarFillImage = GameObject.FindGameObjectWithTag("FillArea").GetComponent<Image>();

    }
    private void Awake()
    {
        colliders=GetComponentsInChildren<SphereCollider>();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if(Health>0)
        {
            OnLevelFail?.Invoke(false);
        }
        if(Health<=0)
        {
            foreach (SphereCollider colliders in colliders)
            {
                colliders.enabled = false;
            }
            OnLevelFail?.Invoke(true);
            StartCoroutine(DisablePlayerCoroutine());
            OnPlayerDeadExplosionSound?.Invoke(deadSound,volume);
            
        }
      

    }

    private void IncreaseHealth(float boostHealth)
    {
        Health += boostHealth;
    }
 
   
    private IEnumerator DisablePlayerCoroutine()
    {
        deadParticle.Play();
        yield return new WaitForSeconds(1f);
        //this.gameObject.SetActive(false);
        this.transform.DOMove(new Vector3(transform.position.z, -4f, transform.position.z), 2f);

    }
    private void OnDisable()
    {
        ButtonManager.OnHealthButtonPressed -= IncreaseHealth;
        DamageDetection.OnPlayerDecreaseHealth -= TakeDamage;
    }
}
