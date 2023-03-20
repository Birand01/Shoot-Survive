using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text waveInfoText,goldCounterText,
        experienceCounter,maxHealtText,remainingHealthText,levelFailText;
    [SerializeField] private GameObject levelFailMenu;

    private void OnEnable()
    {
        PlayerConfiguration.OnPlayerMaxHealthText += MaxHealtText;
        PlayerConfiguration.OnPlayerCurrentHealthText += CurrentHealth;
        PlayerHealth.OnLevelFail += LevelFailEvent;
        EnemySpawner.OnWaveInformation += CurrentWaveNumber;
        TurretBase.OnGoldTextCounter += GoldTextCounter;
        TurretBase.OnExpTextCounter += ExperienceTextCounter;
    }

    private void CurrentWaveNumber(int counter,bool state)
    {
        waveInfoText.gameObject.SetActive(state);
        if (state)
        {
            waveInfoText.text = "WAVE " + counter;
           
        }
        else
        {
            waveInfoText.text = null;
        }
       
    }

    private void LevelFailEvent(bool state)
    {
        levelFailMenu.gameObject.SetActive(state);
        StartCoroutine(LevelFailCoroutine(state));
    }
    private IEnumerator LevelFailCoroutine(bool state)
    {
        if (state)
        {
            yield return new WaitForSeconds(2.5f);
            SceneManager.LoadScene(0);
        }
    }
    
    private void MaxHealtText(float value)
    {
        maxHealtText.text = value.ToString();
       
    }
    private void CurrentHealth(float value)
    {
        remainingHealthText.text =Mathf.FloorToInt(value).ToString();
       
    }

    private void GoldTextCounter(int value)
    {
        goldCounterText.text=value.ToString();
    }
    private void ExperienceTextCounter(float value)
    {
        float f = Mathf.Round(value * 10.0f) * 0.1f;
        experienceCounter.text =f.ToString();
    }
    private void OnDisable()
    {
        PlayerHealth.OnLevelFail -= LevelFailEvent;
        PlayerConfiguration.OnPlayerMaxHealthText -= MaxHealtText;
        PlayerConfiguration.OnPlayerCurrentHealthText -= CurrentHealth;    
        TurretBase.OnGoldTextCounter -= GoldTextCounter;
        EnemySpawner.OnWaveInformation-= CurrentWaveNumber;   
        TurretBase.OnExpTextCounter -= ExperienceTextCounter;
    }
}
    