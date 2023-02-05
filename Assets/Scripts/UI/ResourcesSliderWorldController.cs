using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcesSliderWorldController : MonoBehaviour
{
    [SerializeField] Transform barTransform;
    [SerializeField] TextMeshPro barText;
    [SerializeField] SpriteRenderer iconSpriteRenderer;

    public void SetBarValue(float value, float maxValue)
    {
        barTransform.localScale = new Vector3(value / maxValue, barTransform.localScale.y, barTransform.localScale.z);
    } 
    public void SetBarText(string text) => barText.text = text;
    public void SetBarIcon(Sprite sprite) => iconSpriteRenderer.sprite = sprite;
}
