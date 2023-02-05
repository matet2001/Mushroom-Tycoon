using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMenuController : MonoBehaviour
{
    [SerializeField] ResourceSliderMenuController[] resourceSliderMenuControllers;
    [SerializeField] TreeController treeController;
    private ResourceData resourceData;

    [SerializeField] float deafultSliderMax = 10f;

    private void Start()
    {
        SetUpSliderMenu();
    }
    private void Update()
    {
        for (int i = 0; i < resourceSliderMenuControllers.Length; i++)
        {
            ResourceSliderMenuController currentSlider = resourceSliderMenuControllers[i];
            if (Input.GetMouseButtonDown(0))
            {
                if (currentSlider.IsMouseCloseToPlus())
                {
                    if (currentSlider.barValueMax > treeController.resourceData.resourceUse[currentSlider.resourceType])
                        IncreaseResource(currentSlider.resourceType);
                }
                else if (currentSlider.IsMouseCloseToMinus())
                {
                    if (0 < treeController.resourceData.resourceUse[currentSlider.resourceType])
                        DecreaseResource(currentSlider.resourceType);
                }
            }
        }
       
    }
    private void SetUpSliderMenu()
    {
        resourceData = treeController.resourceData;

        int i = 0;
        foreach(ResourceTypeSO resourceType in resourceData.resourceTypes)
        {
            float resourceGet = resourceData.resourceProduce[resourceType] - resourceData.resourceUsage[resourceType];
            float barValueMax = (resourceGet != 0) ? resourceGet * 2 : deafultSliderMax;

            resourceSliderMenuControllers[i].SetBarIcon(resourceData.resourceTypes[i].resourceImageUI);
            resourceSliderMenuControllers[i].SetBarText(resourceGet.ToString());
            resourceSliderMenuControllers[i].SetBarValueMax(barValueMax);
            resourceSliderMenuControllers[i].SetBarValue(resourceGet);
            resourceSliderMenuControllers[i].resourceType = resourceType;
            resourceData.resourceUse[resourceType] = resourceGet;
            i++;
        }
    }
    private void RefreshSliderMenu()
    {
        int i = 0;
        foreach (ResourceTypeSO resourceType in resourceData.resourceTypes)
        {
            float resourceGet = resourceData.resourceUse[resourceType];

            resourceSliderMenuControllers[i].SetBarText(resourceGet.ToString());
            resourceSliderMenuControllers[i].SetBarValue(resourceGet);
            i++;
        }
    }
    public void IncreaseResource(ResourceTypeSO resourceType)
    {
        treeController.resourceData.resourceUse[resourceType]++;
        RefreshSliderMenu();
    }
    public void DecreaseResource(ResourceTypeSO resourceType)
    {
        treeController.resourceData.resourceUse[resourceType]--;
        RefreshSliderMenu();
    }
}
