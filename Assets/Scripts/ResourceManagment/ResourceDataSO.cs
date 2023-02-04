using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceDataSO", menuName = "ScriptableObjects/ResourceData")]
public class ResourceDataSO : ScriptableObject
{
    [HideInInspector]
    public ResourceTypeSO[] resourceTypes;

    public Dictionary<ResourceTypeSO, float> resourceAmount;
    public Dictionary<ResourceTypeSO, float> resourceUsage;
    public Dictionary<ResourceTypeSO, float> resourceGet;
    public Dictionary<ResourceTypeSO, float> resourceAdd;
    public Dictionary<ResourceTypeSO, float> maxResourceAmount;

    //Fill resource amounts with same value
    public ResourceDataSO(ResourceTypeContainer resourceTypeContainer, float amount, float usage, float get, float add, float max)
    {
        resourceTypes = resourceTypeContainer.resourceTypes;

        resourceAmount = new Dictionary<ResourceTypeSO, float>();
        resourceUsage = new Dictionary<ResourceTypeSO, float>();
        resourceGet = new Dictionary<ResourceTypeSO, float>();
        resourceAdd = new Dictionary<ResourceTypeSO, float>();
        maxResourceAmount = new Dictionary<ResourceTypeSO, float>();

        foreach (ResourceTypeSO resourceType in resourceTypeContainer.resourceTypes)
        {
            resourceAmount[resourceType] = amount;
            resourceUsage[resourceType] = usage;
            resourceGet[resourceType] = get;
            resourceAdd[resourceType] = add;
            maxResourceAmount[resourceType] = max;
        }
    }
    //Fill resource amount with different values
    public ResourceDataSO(ResourceTypeContainer resourceTypeContainer, float[] amount, float[] usage, float[] get, float[] add, float[] max)
    {
        resourceTypes = resourceTypeContainer.resourceTypes;
        
        resourceAmount = new Dictionary<ResourceTypeSO, float>();
        resourceUsage = new Dictionary<ResourceTypeSO, float>();
        resourceGet = new Dictionary<ResourceTypeSO, float>();
        resourceAdd = new Dictionary<ResourceTypeSO, float>();
        maxResourceAmount = new Dictionary<ResourceTypeSO, float>();

        int resourceNumber = 0;
        
        foreach (ResourceTypeSO resourceType in resourceTypeContainer.resourceTypes)
        {
            resourceAmount[resourceType] = amount[resourceNumber];
            resourceUsage[resourceType] = usage[resourceNumber];
            resourceGet[resourceType] = get[resourceNumber];
            resourceAdd[resourceType] = add[resourceNumber];
            maxResourceAmount[resourceType] = max[resourceNumber];

            resourceNumber++;
        }
    }
    public float[] GetResourceUsage()
    {
        float[] returnValue = new float[3];
        int index = 0;
        
        foreach (ResourceTypeSO resourceType in resourceTypes)
        {
            returnValue[index] = resourceUsage[resourceType];
            index++;
        }
        return returnValue;
    }
}