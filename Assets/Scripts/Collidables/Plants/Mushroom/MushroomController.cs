using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : PlantBase
{
    [SerializeField] StartResourceDataSO startResourceData;
    public ResourceData resourceData { get; private set; }

    private void Awake()
    {
        resourceData = new ResourceData(startResourceData);
    }

    private void Start()
    {
        ResourceManager.Instance.connectionManager.AddToMushroomControllerList(this);

        ResourceManager.Instance.OnResourceAmountRefresh += Instance_OnResourceAmountRefresh;
    } 
    public override void Collision()
    {
        
    }
    private void CalculateResourceUsage()
    {
        foreach (ResourceTypeSO resourceType in resourceData.resourceTypes)
        {
            resourceData.resourceUsage[resourceType] = resourceData.resourceProduce[resourceType] - resourceData.resourceUse[resourceType];
        }
    }
    private void Instance_OnResourceAmountRefresh()
    {
        CalculateResourceUsage();
    }
}
