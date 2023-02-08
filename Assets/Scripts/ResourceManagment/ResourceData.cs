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

    //Fill resource amounts with same value
    public ResourceData(ResourceTypeContainer resourceTypeContainer, float amount, float usage, float use, float produce, float max)
    {
        resourceTypes = resourceTypeContainer.resourceTypes;

        resourceAmount = new Dictionary<ResourceTypeSO, float>();
        resourceUsage = new Dictionary<ResourceTypeSO, float>();
        resourceUse = new Dictionary<ResourceTypeSO, float>();
        resourceProduce = new Dictionary<ResourceTypeSO, float>();
        resourceMax = new Dictionary<ResourceTypeSO, float>();

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
    public ResourceData(ResourceTypeContainer resourceTypeContainer, float[] amount, float[] usage, float[] use, float[] produce, float[] max)
    {
        resourceTypes = resourceTypeContainer.resourceTypes;
        
        resourceAmount = new Dictionary<ResourceTypeSO, float>();
        resourceUsage = new Dictionary<ResourceTypeSO, float>();
        resourceUse = new Dictionary<ResourceTypeSO, float>();
        resourceProduce = new Dictionary<ResourceTypeSO, float>();
        resourceMax = new Dictionary<ResourceTypeSO, float>();

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
    public ResourceData(ResourceTypeContainer resourceTypeContainer, StartResourceDataSO startResourceData)
    {
        resourceTypes = resourceTypeContainer.resourceTypes;

        resourceAmount = new Dictionary<ResourceTypeSO, float>();
        resourceUsage = new Dictionary<ResourceTypeSO, float>();
        resourceUse = new Dictionary<ResourceTypeSO, float>();
        resourceProduce = new Dictionary<ResourceTypeSO, float>();
        resourceMax = new Dictionary<ResourceTypeSO, float>();

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
}