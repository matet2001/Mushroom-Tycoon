using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeResourceUIController : MonoBehaviour
{
    private ResourceData resourceData;
    
    [SerializeField] ResourcesSliderWorldController[] resourceSliderControllers;
    [SerializeField] TreeController treeController;

    private void Start()
    {
        SetUpResourceAmountUI();
        ResourceManager.Instance.OnResourceAmountChange += Instance_OnResourceAmountChange;
    }
    private void SetUpResourceAmountUI()
    {
        resourceData = treeController.resourceData;

        for (int i = 0; i < resourceData.resourceTypes.Length; i++)
        {
            ResourcesSliderWorldController slider = resourceSliderControllers[i];
            
            float barValue = resourceData.resourceAmount[resourceData.resourceTypes[i]];
            float barMaxValue = resourceData.resourceMax[resourceData.resourceTypes[i]];

            slider.SetBarValueMax(barMaxValue);
            slider.SetBarValue(barValue);
            slider.SetBarText(barValue.ToString() + "/" + barMaxValue.ToString());
            slider.SetBarIcon(resourceData.resourceTypes[i].resourceImageUI);
        }
    }
    private void Instance_OnResourceAmountChange()
    {
        RefreshResourceAmountUI();
    }
    private void RefreshResourceAmountUI()
    {
        for (int i = 0; i < resourceData.resourceTypes.Length; i++)
        {
            ResourcesSliderWorldController slider = resourceSliderControllers[i];

            float currentValue = resourceData.resourceAmount[resourceData.resourceTypes[i]];
            float maxValue = resourceData.resourceMax[resourceData.resourceTypes[i]];

            if (currentValue <= 0)
            {
                currentValue = 0;
                Debug.Log("Min amount of " + resourceData.resourceTypes[i].resourceName + " reached, at tree resource manager");
            }
            if (currentValue >= maxValue)
            {
                currentValue = maxValue;
                Debug.Log("Max amount of " + resourceData.resourceTypes[i].resourceName + " reached, at tree resource manager");
            }

            slider.SetBarValueMax(maxValue);
            slider.SetBarValue(currentValue);
            slider.SetBarText(currentValue.ToString() + "/" + maxValue.ToString());
        }
    }
}
