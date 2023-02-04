using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    public event Action<ResourceTypeSO[], float[]> OnResourceAmountChange;
    public event Action OnResourceAmountRefresh;

    private ResourceData resourceData;
    private ConnectionManager connectionManager;

    [SerializeField] float resourceRefreshTime = 3;
    private float resourceRefreshTimeMax;

    private void Awake()
    {
        SingletonPattern();
        SetUpResources();
        GetComponents();
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
            DontDestroyOnLoad(gameObject);
        }
    }
    private void SetUpResources()
    {
        resourceData = new ResourceData(Resources.Load<ResourceTypeContainer>("ResourceTypeContainer"), 0, 0, 0, 0, 0);
        resourceRefreshTimeMax = resourceRefreshTime;
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
            RefreshResourceAmount();
        }
    }
    private void RefreshResourceAmount()
    {
        CalculateResourceValues();
        resourceRefreshTime = resourceRefreshTimeMax;
        
        float[] newResourceAmounts = new float[resourceData.resourceAmount.Count];

        for(int i = 0; i < resourceData.resourceTypes.Length; i++)
        {
            float currentResourceAmount = resourceData.resourceUsage[resourceData.resourceTypes[i]];
            SubstractResourceAmount(resourceData.resourceTypes[i], currentResourceAmount);

            currentResourceAmount = resourceData.resourceGet[resourceData.resourceTypes[i]];
            AddResourceAmount(resourceData.resourceTypes[i], currentResourceAmount);

            newResourceAmounts[i] = resourceData.resourceAmount[resourceData.resourceTypes[i]];
        }

        OnResourceAmountRefresh?.Invoke();
        OnResourceAmountChange?.Invoke(resourceData.resourceTypes, newResourceAmounts);
    }
    public void AddResourceAmount(ResourceTypeSO resourceType, float amount)
    {
        resourceData.resourceAmount[resourceType] += amount;
    }
    public void SubstractResourceAmount(ResourceTypeSO resourceType, float amount)
    {
        resourceData.resourceAmount[resourceType] -= amount;
    }
    public float GetResourceAmount(ResourceTypeSO resourceType)
    {
        return resourceData.resourceAmount[resourceType];
    }
    private void CalculateResourceValues()
    {
        for (int i = 0; i < connectionManager.treeControllerList.Count; i++)
        {
            foreach (ResourceTypeSO resourceType in resourceData.resourceTypes)
            {
                resourceData.resourceUsage[resourceType] += connectionManager.treeControllerList[i].resourceData.resourceUsage[resourceType];
                resourceData.resourceAdd[resourceType] += connectionManager.treeControllerList[i].resourceData.resourceAdd[resourceType];
                resourceData.resourceGet[resourceType] += connectionManager.treeControllerList[i].resourceData.resourceGet[resourceType];
                resourceData.resourceMax[resourceType] += connectionManager.treeControllerList[i].resourceData.resourceMax[resourceType];
            }
        }
    }
}