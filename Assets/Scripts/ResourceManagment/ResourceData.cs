using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResourceData
{
    [HideInInspector]
    public ResourceTypeSO[] resourceTypes;

    public Dictionary<ResourceTypeSO, float> resourceAmount;
    public Dictionary<ResourceTypeSO, float> resourceUsage;
    public Dictionary<ResourceTypeSO, float> resourceUse;
    public Dictionary<ResourceTypeSO, float> resourceProduce;
    public Dictionary<ResourceTypeSO, float> resourceMax;

    private ResourceTypeContainer resourceTypeContainer;

    private void SetUpResources()
    {
        resourceTypeContainer = Resources.Load<ResourceTypeContainer>("ResourceTypeContainer");
        resourceTypes = resourceTypeContainer.resourceTypes;

        resourceAmount = new Dictionary<ResourceTypeSO, float>();
        resourceUsage = new Dictionary<ResourceTypeSO, float>();
        resourceUse = new Dictionary<ResourceTypeSO, float>();
        resourceProduce = new Dictionary<ResourceTypeSO, float>();
        resourceMax = new Dictionary<ResourceTypeSO, float>();
    }
    //Fill resource amounts with same value
    public ResourceData(float amount, float usage, float use, float produce, float max)
    {
        SetUpResources();

        foreach (ResourceTypeSO resourceType in resourceTypeContainer.resourceTypes)
        {
            resourceAmount[resourceType] = amount;
            resourceUsage[resourceType] = usage;
            resourceUse[resourceType] = use;
            resourceProduce[resourceType] = produce;
            resourceMax[resourceType] = max;
        }
    }
    //Fill resource amount with different values
    public ResourceData(float[] amount, float[] usage, float[] use, float[] produce, float[] max)
    {
        SetUpResources();

        int resourceNumber = 0;
        
        foreach (ResourceTypeSO resourceType in resourceTypeContainer.resourceTypes)
        {
            resourceAmount[resourceType] = amount[resourceNumber];
            resourceUsage[resourceType] = usage[resourceNumber];
            resourceUse[resourceType] = use[resourceNumber];
            resourceProduce[resourceType] = produce[resourceNumber];
            resourceMax[resourceType] = max[resourceNumber];

            resourceNumber++;
        }
    }
    //Fill resource amount from started resource data
    public ResourceData(StartResourceDataSO startResourceData)
    {
        SetUpResources();

        int resourceNumber = 0;

        foreach (ResourceTypeSO resourceType in resourceTypeContainer.resourceTypes)
        {
            resourceAmount[resourceType] = startResourceData.resourceAmount[resourceNumber];
            resourceUsage[resourceType] = startResourceData.resourceUsage[resourceNumber];
            resourceUse[resourceType] = startResourceData.resourceUse[resourceNumber];
            resourceProduce[resourceType] = startResourceData.resourceProduce[resourceNumber];
            resourceMax[resourceType] = startResourceData.resourceMax[resourceNumber];

            resourceNumber++;
        }
    }
    //Fill resource amount from tree type so
    public ResourceData(TreeTypeSO treeTypeSO)
    {
        SetUpResources();

        int resourceNumber = 0;

        foreach (ResourceTypeSO resourceType in resourceTypeContainer.resourceTypes)
        {
            resourceAmount[resourceType] = treeTypeSO.resourceAmount[resourceNumber]; 
            
            float useBaseValue =  treeTypeSO.resourceUse[resourceNumber];
            resourceUse[resourceType] = RandomizeValue(useBaseValue);
            float produceBaseValue = treeTypeSO.resourceProduce[resourceNumber];
            resourceProduce[resourceType] = RandomizeValue(produceBaseValue);
            resourceMax[resourceType] = treeTypeSO.resourceMax[resourceNumber];

            resourceNumber++;
        }
    }
    private float RandomizeValue(float baseValue)
    {
        float randomizedValue = UnityEngine.Random.Range(baseValue * 0.7f, baseValue * 1.3f);
        return Mathf.Round(randomizedValue);
    }
    
}