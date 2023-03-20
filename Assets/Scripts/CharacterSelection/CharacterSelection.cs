using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public delegate void OnSelectedCarIndexEventHandler(int carIndex);
    public static event OnSelectedCarIndexEventHandler OnSelectedCarIndex;

    [Header("Navigation Buttons")]
    [SerializeField] private Button previousButton, nextButton;

    [Header("Play/Buy Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private TMP_Text priceText;

    [Header("Car Attributes")]
    [SerializeField] private int[] turretPrices;
    private int currentSelectedTurret; 

    private void Start()
    {
        currentSelectedTurret = SaveManager.instance.currentTurret;
        SelectCar(currentSelectedTurret); 
    }
    private void Update()
    {
        if(buyButton.gameObject.activeInHierarchy)
        {
            //Check if we have enough gold
            buyButton.interactable = (SaveManager.instance.goldAmount >= turretPrices[currentSelectedTurret]);

        }
    }
    private void SelectCar(int _index)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == _index);
           
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (SaveManager.instance.turretsUnlocked[currentSelectedTurret])
        {
            playButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);
        }
        else
        {
            playButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(true);
            priceText.text = turretPrices[currentSelectedTurret] + "$";

         
        }
    }

    public void ChangeCar(int _change)
    {
        currentSelectedTurret += _change;

        if(currentSelectedTurret>transform.childCount-1)
        {
            currentSelectedTurret = 0;
        }else if(currentSelectedTurret<0)
        {
            currentSelectedTurret = transform.childCount - 1;
        }
        OnSelectedCarIndex?.Invoke(currentSelectedTurret);
        SelectCar(currentSelectedTurret);
    }
    public void BuyTurret()
    {
        SaveManager.instance.goldAmount -= turretPrices[currentSelectedTurret];
        SaveManager.instance.turretsUnlocked[currentSelectedTurret] = true;
        SaveManager.instance.Save();
        UpdateUI();
    }
  
    
}
