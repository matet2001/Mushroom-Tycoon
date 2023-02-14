using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMenuController : MonoBehaviour
{
    [SerializeField] ResourceSliderMenuController[] resourceSliderMenuControllers;
    [SerializeField] TreeController treeController;
    
    private ResourceData resourceData;

    [SerializeField] float deafultSliderMax = 10f;
    private float defaultSliderValue = 0f;
    private float sliderValueStep = 1;

    private void Start()
    {
        SetUpSliderMenu();
    }
    private void Update()
    {
        CheckForMouseClick();
    }
    private void SetUpSliderMenu()
    {
        resourceData = treeController.resourceData;

        int i = 0;
        foreach (ResourceTypeSO resourceType in resourceData.resourceTypes)
        {
            resourceSliderMenuControllers[i].resourceType = resourceType;

            resourceSliderMenuControllers[i].SetBarIcon(resourceData.resourceTypes[i].resourceImageUI);

            resourceSliderMenuControllers[i].SetBarValueMax(deafultSliderMax/2);
            resourceSliderMenuControllers[i].SetBarValueMin(0 - deafultSliderMax / 2);
            resourceSliderMenuControllers[i].SetBarValue(defaultSliderValue);   
            i++;
        }
    }
    private void CheckForMouseClick()
    {
        for (int i = 0; i < resourceSliderMenuControllers.Length; i++)
        {
            ResourceSliderMenuController currentSlider = resourceSliderMenuControllers[i];

            if (!Input.GetMouseButtonDown(0)) continue;

            if (currentSlider.IsMouseCloseToPlus())
            {
                currentSlider.AddToBarValue(sliderValueStep);
            }
            else if (currentSlider.IsMouseCloseToMinus())
            {
                currentSlider.AddToBarValue(-sliderValueStep);
            }
        }
    }
    public void UpdateResourceTradeValues()
    {
        foreach (ResourceSliderMenuController slider in resourceSliderMenuControllers)
        {
            treeController.resourceTradeAmount[slider.resourceType] = slider.GetBarValue();
        }
    }
}
