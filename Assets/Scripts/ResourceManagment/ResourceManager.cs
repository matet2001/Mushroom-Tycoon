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

    public ResourceData resourceData { get; private set; }

    private Dictionary<ResourceTypeSO, float> resourceAmount;
    private Dictionary<ResourceTypeSO, float> resourceUsage;
    private Dictionary<ResourceTypeSO, float> resourceGet;
    private Dictionary<ResourceTypeSO, float> currentMaximumResource;
    private Dictionary<ResourceTypeSO, float> maximumResource;
    private Dictionary<ResourceTypeSO, float> maximumPossibleResource;

    public ConnectionManager connectionManager { get; private set; }

    [SerializeField] float resourceRefreshTime = 3;
    private float resourceRefreshTimeMax;
    [Space]
    public List<float> maximumResourceCapacityLimit; //Ennél több resourcet nem lehet összeszedni.
    [Space] 
    [SerializeField] List<float> currentMaximumResourceCapacity; //Mennyi a személyes maximum (amikor kevesebb a fa, mint a kapacitás)
    [SerializeField] List<float> resourceUsageAmount;
    [SerializeField] List<float> resourceAdditionAmount;

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
        }
    }
    private void SetUpResources()
    {
        resourceData = new ResourceData(Resources.Load<ResourceTypeContainer>("ResourceTypeContainer"), 10, 1, 0, 0, 50);
        
        resourceAmount = new Dictionary<ResourceTypeSO, float>();
        resourceUsage = new Dictionary<ResourceTypeSO, float>();
        resourceGet = new Dictionary<ResourceTypeSO, float>();
        currentMaximumResource = new Dictionary<ResourceTypeSO, float>();
        maximumResource = new Dictionary<ResourceTypeSO, float>();
        maximumPossibleResource = new Dictionary<ResourceTypeSO, float>();

        int i = 0;

        foreach (ResourceTypeSO resourceType in resourceData.resourceTypes)
        {
            resourceAmount.Add(resourceType, currentMaximumResourceCapacity[i]);
            resourceUsage.Add(resourceType, resourceUsageAmount[i]);
            resourceGet.Add(resourceType, resourceAdditionAmount[i]);
            currentMaximumResource.Add(resourceType, currentMaximumResourceCapacity[i]);
            
            maximumResource.Add(resourceType ,currentMaximumResourceCapacity[i]);
            maximumPossibleResource.Add(resourceType, maximumResourceCapacityLimit[i]);
            i++;
        }

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
    public string[] GetResourceNames()
    {
        string[] resourceNames = new string[3];
        int resourceNumber = 0;

        foreach (ResourceTypeSO resourceType in resourceData.resourceTypes)
        {
            resourceNames[resourceNumber] = resourceType.resourceName;
            resourceNumber++;
        }

        return resourceNames;
    }
    public float GetResourceCurrentMaximumAmount(ResourceTypeSO resourceType)
    {
        return currentMaximumResource[resourceType];
    }
    // elérhető maximum
    public float GetMaximumResourceAmount(ResourceTypeSO resourceType)
    {
        return maximumPossibleResource[resourceType];
    }
    public float[] GetMaximumResourceAmounts()
    {
        var _resourceAmount = new float[3];
        for (int j = 0; j < maximumResourceCapacityLimit.Count; j++)
        {
            _resourceAmount[j] = maximumResourceCapacityLimit[j];
        }

        return _resourceAmount;

    }
    //Jelenlegi maximum
    public float GetCurrentMaximumResourceAmount(ResourceTypeSO resourceType)
    {
        return maximumResource[resourceType];
    }
    public float[] GetCurrentMaximumResourceAmounts()
    {
        var currentResourceAmount = new float[3];
        var i = 0;
        foreach (var items in currentMaximumResourceCapacity)
        {
            currentResourceAmount[i] = items;
            i++;
        }

        return currentResourceAmount;
    }
    //jelenlegi resource
    public float GetResoruceAmount(ResourceTypeSO resourceType)
    {
        return resourceAmount[resourceType];
    }
    public float[] GetResourceAmounts()
    {
        float[] resourceAmounts = new float[3];
        int resourceNumber = 0;

        foreach (ResourceTypeSO resourceType in resourceData.resourceTypes)
        {
            resourceAmounts[resourceNumber] = resourceAmount[resourceType];
            resourceNumber++;
        }

        return resourceAmounts;
    }
}
