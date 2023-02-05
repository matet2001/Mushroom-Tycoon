using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcesSliderWorldController : MonoBehaviour
{
    [SerializeField] Transform barTransform;
    [SerializeField] TextMeshPro barText;
    [SerializeField] SpriteRenderer iconSpriteRenderer;
    public ResourceTypeSO resourceType;

    public float barValueMax;

    public void SetBarValueMax(float value) => barValueMax = value;
    public void SetBarValue(float value)
    {
        if(value == 0)
        {
            barTransform.localScale = new Vector3(0f, barTransform.localScale.y, barTransform.localScale.z);
        }
        else
        {
            barTransform.localScale = new Vector3(value / barValueMax, barTransform.localScale.y, barTransform.localScale.z);
        }
        
    } 
    public void SetBarText(string text) => barText.text = text;
    public void SetBarIcon(Sprite sprite) => iconSpriteRenderer.sprite = sprite;
}
