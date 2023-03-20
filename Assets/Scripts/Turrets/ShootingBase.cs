using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BulletPool))]
public abstract class ShootingBase : MonoBehaviour
{
    protected BulletPool bulletPool;
    [SerializeField] private int bulletPoolCount = 10;
    [SerializeField] private List<Transform> shootPoints;
    [SerializeField] internal GameObject bulletPrefab;
    [SerializeField] internal float bulletSpeed;
    internal AudioClip bulletSound;
    internal float volume;
    public ParticleSystem shootParticle;
    private Collider[] colliders;
    protected virtual void Awake()  
    {
        bulletPool = GetComponent<BulletPool>();
        colliders = GetComponentsInParent<Collider>();
    }
    protected virtual void Start()
    {
        bulletPool.Inýtýalize(bulletPrefab, bulletPoolCount);
    }
    protected virtual void Shoot(IDamageable damageable)
    {
        if (damageable != null)
        {
            foreach (var barrel in shootPoints)
            {
                
                GameObject bullet = bulletPool.CreateObject();
                bullet.transform.position = barrel.position;
                bullet.transform.SetParent(null);
                Vector3 direction = damageable.GetTransform().position - bullet.transform.position;
                bullet.transform.LookAt(damageable.GetTransform());
                bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
                foreach (var collider in colliders)
                {
                    Physics.IgnoreCollision(bullet.GetComponent<Collider>(), collider);
                }
            }
        }

    }
}
