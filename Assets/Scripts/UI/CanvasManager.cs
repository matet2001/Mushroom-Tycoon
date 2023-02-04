using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] Transform[] resourceAmountTransforms;

    private void Start()
    {
        SetUpResourceAmountUI();

        ResourceManager.Instance.OnResourceAmountChange += Instance_OnResourceAmountChange;
    }
    private void SetUpResourceAmountUI()
    {
        string[] resourceNames = ResourceManager.Instance.GetResourceNames();
        float[] resourceAmounts = ResourceManager.Instance.GetResourceAmounts();

        for (int i = 0; i < resourceAmountTransforms.Length; i++)
        {
            TextMeshProUGUI[] resourceAmountTexts = resourceAmountTransforms[i].GetComponentsInChildren<TextMeshProUGUI>();
            resourceAmountTexts[0].text = resourceNames[i];
            resourceAmountTexts[1].text = resourceAmounts[i].ToString();
        }
    }
    private void Instance_OnResourceAmountChange(ResourceTypeSO[] arg1, float[] resourceAmount)
    {
        for (int i = 0; i < resourceAmountTransforms.Length; i++)
        {
            TextMeshProUGUI[] resourceAmountTexts = resourceAmountTransforms[i].GetComponentsInChildren<TextMeshProUGUI>();
            resourceAmountTexts[1].text = resourceAmount[i].ToString();
        }
    }
}
