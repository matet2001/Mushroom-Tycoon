using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GeneralSlider : MonoBehaviour
{
    public float sliderProgress;
    [Space] 
    public TextMeshProUGUI sliderText;
    [Space] 
    public Image slider;
    public Image sliderIcon;

    public void SetSliderFill(float currentValue, float maximumValue) 
        => slider.fillAmount = currentValue / maximumValue;

    public void SetSliderText(string textContent)
        => sliderText.text = textContent;

    public void SetSliderIcon(Sprite icon)
        => sliderIcon.sprite = icon;
}
