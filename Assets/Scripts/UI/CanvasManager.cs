using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private ResourceData resourceData;
    private ResourceSliderController[] resourceSliderControllers;

    private void Start()
    {
        SetUpResourceAmountUI();

        ResourceManager.Instance.OnResourceAmountChange += Instance_OnResourceAmountChange;
    }
    private void SetUpResourceAmountUI()
    {
        resourceData = ResourceManager.Instance.resourceData;
        resourceSliderControllers = GetComponentsInChildren<ResourceSliderController>();

        for (int i = 0; i < resourceData.resourceTypes.Length; i++)
        {
            resourceSliderControllers[i].SetSliderMaxValue(resourceData.resourceMax[resourceData.resourceTypes[i]]);
            resourceSliderControllers[i].SetSliderValue(resourceData.resourceAmount[resourceData.resourceTypes[i]]);
            resourceSliderControllers[i].SetSliderText(resourceData.resourceAmount[resourceData.resourceTypes[i]].ToString() + "/" + resourceData.resourceMax[resourceData.resourceTypes[i]].ToString());
            resourceSliderControllers[i].SetIconImage(resourceData.resourceTypes[i].resourceImageUI);
        }
    }
    private void Instance_OnResourceAmountChange(ResourceTypeSO[] arg1, float[] resourceAmount)
    {
        for (int i = 0; i < resourceData.resourceTypes.Length; i++)
        {
            float maxValue = resourceData.resourceMax[resourceData.resourceTypes[i]];
            float currentValue = resourceData.resourceAmount[resourceData.resourceTypes[i]];

            if(currentValue <= 0)
            {
                currentValue = 0;
                Debug.Log("Min amount of " + resourceData.resourceTypes[i].resourceName + " reached, at resource manager");
            }
            if (currentValue >= maxValue)
            {
                currentValue = maxValue;
                Debug.Log("Max amount of " + resourceData.resourceTypes[i].resourceName + " reached, at resource manager");
            }

            resourceSliderControllers[i].SetSliderMaxValue(maxValue);
            resourceSliderControllers[i].SetSliderValue(currentValue);
            resourceSliderControllers[i].SetSliderText(currentValue.ToString() + "/" + maxValue.ToString());
        }
    }
}