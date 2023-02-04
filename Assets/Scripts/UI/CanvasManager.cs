using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] Transform[] resourceAmountTransforms;
    [Space] public Image[] resourceAmountSliders;

    private void Start()
    {
        SetUpResourceAmountUI();

        ResourceManager.Instance.OnResourceAmountChange += Instance_OnResourceAmountChange;
    }

    private void SetUpResourceAmountUI()
    {
        string[] resourceNames = ResourceManager.Instance.GetResourceNames();
        int[] currentResourceAmounts = ResourceManager.Instance.GetResourceAmounts();
        int[] currentMaximumResourceAmounts = ResourceManager.Instance.GetCurrentMaximumResourceAmounts();
        int[] maximumPossibleResourceAmounts = ResourceManager.Instance.GetMaximumResourceAmounts();

        for (int i = 0; i < resourceAmountTransforms.Length; i++)
        {
            TextMeshProUGUI[] resourceAmountTexts = resourceAmountTransforms[i].GetComponentsInChildren<TextMeshProUGUI>();
            resourceAmountTexts[0].text = resourceNames[i];
            resourceAmountSliders[i].fillAmount = 
                // ReSharper disable once PossibleLossOfFraction
                (currentResourceAmounts[i] / currentMaximumResourceAmounts[i]) /
                                                   maximumPossibleResourceAmounts[i];
        }
    }
    private void RefreshResourceAmountUI()
    {

    }
    private void Instance_OnResourceAmountChange(ResourceTypeSO[] arg1, int[] resourceAmount)
    {
        string[] resourceNames = ResourceManager.Instance.GetResourceNames();
        int[] currentResourceAmounts = ResourceManager.Instance.GetResourceAmounts();
        int[] currentMaximumResourceAmounts = ResourceManager.Instance.GetCurrentMaximumResourceAmounts();
        // int[] maximumPossibleResourceAmounts = ResourceManager.Instance.GetMaximumResourceAmounts();
        
        for (int i = 0; i < resourceAmountTransforms.Length; i++)
        {
            TextMeshProUGUI[] resourceAmountTexts = resourceAmountTransforms[i].GetComponentsInChildren<TextMeshProUGUI>();
            resourceAmountTexts[0].text = resourceNames[i];
            resourceAmountSliders[i].fillAmount = 
               (currentResourceAmounts[i] / currentMaximumResourceAmounts[i]);
        }
    }
}
