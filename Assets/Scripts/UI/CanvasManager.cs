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
    [SerializeField] ResourceSliderController[] resourceSliderControllers;

    private void Start()
    {
        SetUpResourceAmountUI();

        ResourceManager.Instance.OnResourceAmountChange += Instance_OnResourceAmountChange;
        
    }

    private void SetUpResourceAmountUI()
    {
        resourceData = ResourceManager.Instance.resourceData;
        resourceSliderControllers = GetComponentsInChildren<ResourceSliderController>();

        int number = 0;

        for (int i = 0; i < resourceData.resourceTypes.Length; i++)
        {
            resourceSliderControllers[number].SetSliderMaxValue(resourceData.resourceMax[resourceData.resourceTypes[i]]);
            resourceSliderControllers[number].SetSliderValue(resourceData.resourceAmount[resourceData.resourceTypes[i]]);
            resourceSliderControllers[number].SetSliderText(resourceData.resourceAmount[resourceData.resourceTypes[i]].ToString() + "/" + resourceData.resourceMax[resourceData.resourceTypes[i]].ToString());
            resourceSliderControllers[number].SetIconImage(resourceData.resourceTypes[i].resourceImageUI);
            number++;
        }
    }
    private void Instance_OnResourceAmountChange(ResourceTypeSO[] arg1, float[] resourceAmount)
    {
        
    }
}