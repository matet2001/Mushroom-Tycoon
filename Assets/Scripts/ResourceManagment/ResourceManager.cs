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
        resourceData = new ResourceData(Resources.Load<ResourceTypeContainer>("ResourceTypeContainer"), startResourceData);
        resourceRefreshTimeMax = resourceRefreshTime;
        RefreshResourceData();
    }
    private void Update()
    {
        CountDownResourceAmountRefresh();
    }
    private void CountDownResourceAmountRefresh()
    {
        if (resourceRefreshTime > 0f) resourceRefreshTime -= Time.deltaTime;
        else
        {
            RefreshResourceData();
        }
    }
    private void CalculateResourceValues()
    {    
        foreach (ResourceTypeSO resourceType in resourceData.resourceTypes)
        {
            for (int i = 0; i < connectionManager.treeControllerList.Count; i++)
            {
                resourceData.resourceUsage[resourceType] += connectionManager.treeControllerList[i].resourceData.resourceUsage[resourceType];
                resourceData.resourceProduce[resourceType] += connectionManager.treeControllerList[i].resourceData.resourceProduce[resourceType];
                resourceData.resourceUse[resourceType] += connectionManager.treeControllerList[i].resourceData.resourceUse[resourceType];
                resourceData.resourceMax[resourceType] += connectionManager.treeControllerList[i].resourceData.resourceMax[resourceType];
            }
            for (int i = 0; i < connectionManager.mushroomControllerList.Count; i++)
            {
                resourceData.resourceUsage[resourceType] += connectionManager.mushroomControllerList[i].resourceData.resourceUsage[resourceType];
                resourceData.resourceUse[resourceType] += connectionManager.mushroomControllerList[i].resourceData.resourceUse[resourceType];
                resourceData.resourceMax[resourceType] += connectionManager.mushroomControllerList[i].resourceData.resourceMax[resourceType];
            }
        }
        
    }
    private void RefreshResourceData()
    {
        CalculateResourceValues();
        resourceRefreshTime = resourceRefreshTimeMax;
        
        float[] newResourceAmounts = new float[resourceData.resourceAmount.Count];

        for(int i = 0; i < resourceData.resourceTypes.Length; i++)
        {
            float currentResourceAmount = resourceData.resourceUsage[resourceData.resourceTypes[i]];
            SubstractResourceAmount(resourceData.resourceTypes[i], currentResourceAmount);

            currentResourceAmount = resourceData.resourceUse[resourceData.resourceTypes[i]];
            AddResourceAmount(resourceData.resourceTypes[i], currentResourceAmount);

            newResourceAmounts[i] = resourceData.resourceAmount[resourceData.resourceTypes[i]];
        }

        OnResourceAmountRefresh?.Invoke();
        OnResourceAmountChange?.Invoke();
    }
    public void AddResourceAmount(ResourceTypeSO resourceType, float amount)
    {
        resourceData.resourceAmount[resourceType] += amount;
    }
    public void SubstractResourceAmount(ResourceTypeSO resourceType, float amount)
    {
        resourceData.resourceAmount[resourceType] -= amount;
    }  
    public void ConsumeResource(float[] resourceToConsume)
    {
        for (int i = 0; i < resourceData.resourceTypes.Length; i++)
        {
            SubstractResourceAmount(resourceData.resourceTypes[i], resourceToConsume[i]);
        }
        OnResourceAmountChange?.Invoke();
    }
}
