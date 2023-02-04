using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] Transform[] resourceAmountTransforms;
    [Space] public GeneralSlider[] resourceSliders;

    /// <summary>
    /// Below: debug variables
    /// TODO: delete later
    /// </summary>
    public TextMeshProUGUI[] currentResourcesText;
    public TextMeshProUGUI[] maximumResourcesText;

    private ResourceData resourceData;

    private void Start()
    {
        SetUpResourceAmountUI();

        ResourceManager.Instance.OnResourceAmountChange += Instance_OnResourceAmountChange;
        resourceData = ResourceManager.Instance.resourceData;
    }

    private void SetUpResourceAmountUI()
    {
        for (int i = 0; i < resourceAmountTransforms.Length; i++)
        {
            //Slider
            resourceSliders[i].SetSliderFill(
                ResourceManager.Instance.GetResourceAmounts()[i],
                ResourceManager.Instance.GetMaximumResourceAmounts()[i]);
            resourceSliders[i].SetSliderText(
                $"{ResourceManager.Instance.GetResourceAmounts()[i]}/" +
                $"{ResourceManager.Instance.GetMaximumResourceAmounts()[i]}");
        }
    }
    private void Instance_OnResourceAmountChange(ResourceTypeSO[] arg1, float[] resourceAmount)
    {
        for (int i = 0; i < resourceAmountTransforms.Length; i++)
        {
            //Slider
            resourceSliders[i].SetSliderFill(
                ResourceManager.Instance.GetResourceAmounts()[i],
                ResourceManager.Instance.GetMaximumResourceAmounts()[i]);
            resourceSliders[i].SetSliderText(
                $"{ResourceManager.Instance.GetResourceAmounts()[i]}/" +
                $"{ResourceManager.Instance.GetMaximumResourceAmounts()[i]}");
        }
    }
}