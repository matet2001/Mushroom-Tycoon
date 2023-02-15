using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    public ResourceData resourceData { get; private set; }
    [SerializeField] StartResourceDataSO startResourceData;

    public event Action OnResourceAmountChange;
    public event Action OnResourceAmountRefresh;

    public ConnectionManager connectionManager { get; private set; }

    [SerializeField] float resourceRefreshTime = 3;
    private float resourceRefreshTimeMax;

    private void Awake()
    {
        GetComponents();
        SingletonPattern();
        SetUpResources();     
    }
    private void GetComponents()
    {
        connectionManager = GetComponent<ConnectionManager>();
    }
    private void SingletonPattern()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else
        {
            Instance = this;
        }
    }
    private void SetUpResources()
    {
        resourceData = new ResourceData(startResourceData);
        resourceRefreshTimeMax = resourceRefreshTime;

        CalculateResourceAmountMax();
        
        OnResourceAmountRefresh?.Invoke();
        OnResourceAmountChange?.Invoke();
    }
    private void Update()
    {
        CountDownResourceAmountRefresh();
    }
    private void CountDownResourceAmountRefresh()
    {
        if (resourceRefreshTime > 0f) resourceRefreshTime -= Time.deltaTime;
        else RefreshResourceData();
    }
    private void RefreshResourceData()
    {
        OnResourceAmountRefresh?.Invoke();

        resourceRefreshTime = resourceRefreshTimeMax;

        CalculateResourceDatas();

        OnResourceAmountChange?.Invoke();
    }
    private void CalculateResourceUsage()
    {
        ResourceData newResourceData = new ResourceData(startResourceData);

        foreach (ResourceTypeSO resourceType in resourceData.resourceTypes)
        {
            for (int i = 0; i < connectionManager.treeControllerList.Count; i++)
            {
                TreeController currentTreeController = connectionManager.treeControllerList[i];
                newResourceData.resourceUsage[resourceType] -= currentTreeController.resourceTradeAmount[resourceType];
            }
            for (int i = 0; i < connectionManager.mushroomControllerList.Count; i++)
            {
                ResourceData currentMushroomResourceData = connectionManager.mushroomControllerList[i].resourceData;             
                newResourceData.resourceUsage[resourceType] += currentMushroomResourceData.resourceUsage[resourceType];
            }
        }

        resourceData.resourceUsage = newResourceData.resourceUsage;
    }
    private void CalculateResourceAmountMax()
    {
        ResourceData newResourceData = new ResourceData(startResourceData);

        foreach (ResourceTypeSO resourceType in resourceData.resourceTypes)
        {
            for (int i = 0; i < connectionManager.treeControllerList.Count; i++)
            {
                TreeController currentTreeController = connectionManager.treeControllerList[i];
                ResourceData currentTreeResourceData = currentTreeController.resourceData;
                newResourceData.resourceMax[resourceType] += currentTreeResourceData.resourceMax[resourceType];
            }
            for (int i = 0; i < connectionManager.mushroomControllerList.Count; i++)
            {
                ResourceData currentMushroomResourceData = connectionManager.mushroomControllerList[i].resourceData;
                newResourceData.resourceMax[resourceType] += currentMushroomResourceData.resourceMax[resourceType];
            }
        }

        resourceData.resourceMax = newResourceData.resourceMax;
    }
    private void CalculateResourceDatas()
    {
        CalculateResourceUsage();
        CalculateResourceAmountMax();
        CalculateResourceAmount();
    }
    private void CalculateResourceAmount()
    {
        foreach (ResourceTypeSO resourceType in resourceData.resourceTypes)
        {
            resourceData.resourceAmount[resourceType] += resourceData.resourceUsage[resourceType];
        }
    }
    public void ConsumeResource(float[] resourceToConsume)
    {
        for (int i = 0; i < resourceData.resourceTypes.Length; i++)
        {
            SubstractResourceAmount(resourceData.resourceTypes[i], resourceToConsume[i]);
        }
        OnResourceAmountChange?.Invoke();
    }
    public void SubstractResourceAmount(ResourceTypeSO resourceType, float amount)
    {
        resourceData.resourceAmount[resourceType] -= amount;
    }
}
