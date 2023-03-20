using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerAudioConfiguration", menuName = "PlayerAudioConfiguration/Audio", order = 1)]
public class PlayerAudioConfiguration : ScriptableObject
{
    [Range(0f, 1f)] public float volume = 1f;
    public AudioClip deadSound;
    public AudioClip bulletSound;


}
