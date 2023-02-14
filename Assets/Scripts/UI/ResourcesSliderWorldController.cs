using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcesSliderWorldController : MonoBehaviour
{
    public ResourceTypeSO resourceType { get; set; }
    public float barValueMax { get; protected set; }
    
    protected float barValue;

    public Transform barTransform;
    [SerializeField] TextMeshPro barText;
    [SerializeField] SpriteRenderer iconSpriteRenderer;

    public void SetBarValueMax(float value) => barValueMax = value;
    public virtual void SetBarValue(float value)
    {     
        if (value == 0)
        {
            barTransform.localScale = new Vector3(0f, barTransform.localScale.y, barTransform.localScale.z);
        }
        else
        {
            barTransform.localScale = new Vector3(value / barValueMax, barTransform.localScale.y, barTransform.localScale.z);
        }

        barValue = value;
    } 
    public void SetBarText(string text) => barText.text = text;
    public void SetBarIcon(Sprite sprite) => iconSpriteRenderer.sprite = sprite;
    public float GetBarValue() => barValue;
}
