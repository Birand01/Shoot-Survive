using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionBase : MonoBehaviour
{
    [Header("Feedback")]
    [SerializeField] protected AudioClip _pickupSFX = null;
    [SerializeField] protected ParticleSystem _particlePrefab = null;
     protected Collider _collider = null;
    AudioSource _audioSource = null;
    protected virtual void Awake()
    {
        _collider = GetComponent<Collider>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterAction(other);

    }
    private void OnTriggerStay(Collider other)
    {
        OnTriggerStayAction(other);
    }
    private void OnTriggerExit(Collider other)
    {
        OnTriggerExitAction(other);
    }

    protected virtual void OnTriggerStayAction(Collider other) { }
    protected virtual void OnTriggerExitAction(Collider other) { }
    protected abstract void OnTriggerEnterAction(Collider collider);

    private void Reset()
    {
        // set isTrigger in the Inspector, just in case Designer forgot
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    protected virtual void SpawnAudio(AudioClip pickupSFX)
    {
        //AudioSource.PlayClipAtPoint(pickupSFX, transform.position);
        _audioSource.clip = pickupSFX;
        _audioSource.Play();
    }

    protected virtual void SpawnParticle(ParticleSystem pickupParticles)
    {
        ParticleSystem newParticles =
            Instantiate(pickupParticles, transform.position, transform.rotation);
        newParticles.Play();
    }

}
