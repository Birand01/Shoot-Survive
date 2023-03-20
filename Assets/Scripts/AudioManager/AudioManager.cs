using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource backgroundMusicMusic, sfxSource;
    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeadExplosionSound += PlaySFX;  
        Turret.OnPlayerShootSound+=PlaySFX;
    }

    private void PlaySFX(AudioClip clip,float volume)
    {
        sfxSource.PlayOneShot(clip,volume);
    }
    private void PlayMusic(AudioClip music)
    {
        backgroundMusicMusic.clip = music;
        backgroundMusicMusic.Play();
    }
    private void OnDisable()
    {
        Turret.OnPlayerShootSound -= PlaySFX;
        PlayerHealth.OnPlayerDeadExplosionSound -= PlaySFX;
    }
}
