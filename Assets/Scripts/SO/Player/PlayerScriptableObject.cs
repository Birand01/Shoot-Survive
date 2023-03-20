using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "PlayerConfiguration", menuName = "ScriptableObjects/PlayerConfiguration", order = 3)]
public class PlayerScriptableObject : ScriptableObject
{
    public PlayerAudioConfiguration audioConfiguration;
    public float rotationSpeed;
    public float health;
    public float maxHealth;
    public float bulletDamage;
    public float bulletSpeed;
    public float minimumDistanceToAttack;
    public float attackDelay;
    public GameObject bulletPrefab;

    internal void SetUpPlayerFromConfiguration(PlayerConfiguration playerConfiguration)
    {
        playerConfiguration.GetComponent<PlayerHealth>().Health = health;

        Slider healthSlider = GameObject.FindGameObjectWithTag("HealthSlider").GetComponent<Slider>();
        if (healthSlider != null)
        {
            healthSlider.maxValue = health;
            healthSlider.value = playerConfiguration.GetComponent<PlayerHealth>().Health;
        }
        //playerConfiguration.GetComponent<PlayerHealth>().healthBarSlider.maxValue = health;
        //playerConfiguration.GetComponent<PlayerHealth>().healthBarSlider.value = playerConfiguration.GetComponent<PlayerHealth>().Health;
        playerConfiguration.GetComponent<PlayerHealth>().deadSound = audioConfiguration.deadSound;
        playerConfiguration.GetComponent<PlayerHealth>().volume= audioConfiguration.volume;
        playerConfiguration.GetComponentInChildren<Turret>().volume= audioConfiguration.volume;
        playerConfiguration.GetComponentInChildren<PlayerRotation>().rotationSpeed = rotationSpeed;
        playerConfiguration.GetComponentInChildren<Turret>().bulletPrefab = bulletPrefab;
        playerConfiguration.GetComponentInChildren<Turret>().bulletSound = audioConfiguration.bulletSound;
        bulletPrefab.GetComponent<BulletInteraction>().damage=bulletDamage;
        playerConfiguration.GetComponentInChildren<Turret>().bulletSpeed = bulletSpeed;
        playerConfiguration.GetComponentInChildren<AttackRadius>().attackDelay = attackDelay;
        playerConfiguration.GetComponentInChildren<AttackRadius>().minDistanceToAttack = minimumDistanceToAttack;


       

    }
}
