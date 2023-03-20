using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BulletPool))]
public class Turret : ShootingBase
{
    public delegate void OnPlayerShootSoundHandler(AudioClip clip,float volume);
    public static event OnPlayerShootSoundHandler OnPlayerShootSound;
   
    private void OnEnable()
    {
         AttackRadius.OnPlayerAttack += Shoot;
      
    }
    protected override void Shoot(IDamageable damageable)
    {
        if(this.gameObject.GetComponentInParent<PlayerHealth>().Health>0)
        {
            shootParticle.Play();
            OnPlayerShootSound?.Invoke(bulletSound, volume);
            base.Shoot(damageable);
        }
      
    }

    

    private void OnDisable()
    {
       
         AttackRadius.OnPlayerAttack -= Shoot;
    }
}
