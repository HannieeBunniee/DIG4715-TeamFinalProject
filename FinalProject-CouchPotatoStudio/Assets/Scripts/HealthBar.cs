using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // to use UI text

public class HealthBar : MonoBehaviour
{
    //HealthBar
    public Slider sliderBar;
    public Gradient gradient;

    public Image fill;

    //=========Functions==========
    public void SetMaxHealth(int health)
    {
        sliderBar.maxValue = health;
        sliderBar.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetPlayerHealth(int health)
    {
        sliderBar.value = health;

        fill.color = gradient.Evaluate(sliderBar.normalizedValue);
    }

    
}