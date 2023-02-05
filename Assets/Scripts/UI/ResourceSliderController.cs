using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceSliderController : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI sliderText;
    [SerializeField] Image iconImage;

    public void SetSliderMaxValue(float value) => slider.maxValue = value;
    public void SetSliderValue(float value) => slider.value = value;
    public void SetSliderText(string text) => sliderText.text = text;
    public void SetIconImage(Sprite sprite) => iconImage.sprite = sprite;
}
