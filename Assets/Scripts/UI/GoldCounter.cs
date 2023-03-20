using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GoldCounter : MonoBehaviour
{
    
    private TMP_Text goldText;
    private void Awake()
    {
        goldText = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        goldText.text = SaveManager.instance.goldAmount +  "$";
    }
    
}
