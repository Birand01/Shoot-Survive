using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public delegate void OnHealthButtonPressedHandler(float amount);
    public static event OnHealthButtonPressedHandler OnHealthButtonPressed;

   
    public void IncreaseHealthButton(float amount)
    {
        OnHealthButtonPressed?.Invoke(amount);
    }
}
