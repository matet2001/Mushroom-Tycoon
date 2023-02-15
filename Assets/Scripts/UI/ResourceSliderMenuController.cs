using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSliderMenuController : ResourcesSliderWorldController
{
    [SerializeField] Transform plusIconTransform;
    [SerializeField] Transform minusIconTransform;

    public float barValueMin { get; private set; }
    [SerializeField] float distanceToClick = 0.2f;

    public void SetBarValueMin(float value) => barValueMin = value;
    public override void SetBarValue(float value)
    {
        if (value < barValueMin) value = barValueMin;
        if (value > barValueMax) value = barValueMax;

        if (value == barValueMin)
        {
            barTransform.localScale = new Vector3(0f, barTransform.localScale.y, barTransform.localScale.z);
        }
        else
        {
            float barRange = barValueMax - barValueMin;
            float barValueNormalized = (barValueMax + value) / barRange;

            barTransform.localScale = new Vector3(barValueNormalized, barTransform.localScale.y, barTransform.localScale.z);
        }

        barValue = value;
        SetBarText(value.ToString());
    }
    public void AddToBarValue(float value)
    {
        barValue += value;
        SetBarValue(barValue);
    }
    public bool IsMouseCloseToPlus()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        return Vector2.Distance(mousePosition, plusIconTransform.position) < distanceToClick;
    }
    public bool IsMouseCloseToMinus()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        return Vector2.Distance(mousePosition, minusIconTransform.position) < distanceToClick;
    }
}