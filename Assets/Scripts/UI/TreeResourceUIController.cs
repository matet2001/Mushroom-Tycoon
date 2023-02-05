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
        ResourceManager.Instance.OnResourceAmountRefresh += Instance_OnResourceAmountRefresh;
    }
    private void SetUpResourceAmountUI()
    {
        resourceData = treeController.resourceData;

        for (int i = 0; i < resourceData.resourceTypes.Length; i++)
        {
            ResourcesSliderWorldController slider = resourceSliderControllers[i];
            
            float barValue = resourceData.resourceAmount[resourceData.resourceTypes[i]];
            float barMaxValue = resourceData.resourceMax[resourceData.resourceTypes[i]];

            slider.SetBarValue(barValue, barMaxValue);
            slider.SetBarText(barValue.ToString() + "/" + barMaxValue.ToString());
            slider.SetBarIcon(resourceData.resourceTypes[i].resourceImageUI);
        }
    }
    private void Instance_OnResourceAmountRefresh()
    {
        RefreshResourceAmountUI();
    }
    private void RefreshResourceAmountUI()
    {
        for (int i = 0; i < resourceData.resourceTypes.Length; i++)
        {
            ResourcesSliderWorldController slider = resourceSliderControllers[i];

            float barValue = resourceData.resourceAmount[resourceData.resourceTypes[i]];
            float barMaxValue = resourceData.resourceMax[resourceData.resourceTypes[i]];

            slider.SetBarValue(barValue, barMaxValue);
            slider.SetBarText(barValue.ToString() + "/" + barMaxValue.ToString());
        }
    }
}
