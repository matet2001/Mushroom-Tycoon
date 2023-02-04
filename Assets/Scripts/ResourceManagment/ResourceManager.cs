using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    [SerializeField] ResourceTypeSO[] resourceTypes = new ResourceTypeSO[3];

    private Dictionary<ResourceTypeSO, int> resourceAmount;
    private Dictionary<ResourceTypeSO, int> resourceUsage;
    private Dictionary<ResourceTypeSO, int> resourceGet;
    private Dictionary<ResourceTypeSO, int> currentMaximumResource;
    private Dictionary<ResourceTypeSO, int> maximumResource;
    private Dictionary<ResourceTypeSO, int> maximumPossibleResource;

    public event Action<ResourceTypeSO[], int[]> OnResourceAmountChange;

    [SerializeField] float resourceRefreshTime = 3;
    private float resourceRefreshTimeMax;
    [Space]
    public List<int> maximumResourceCapacityLimit; //Ennél több resourcet nem lehet összeszedni.
    [Space] 
    [SerializeField] List<int> currentMaximumResourceCapacity; //Mennyi a személyes maximum (amikor kevesebb a fa, mint a kapacitás)
    [SerializeField] List<int> resourceUsageAmount;
    [SerializeField] List<int> resourceAdditionAmount;

    private void Awake()
    {
        SingletonPattern();
        SetUpResources();
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
        resourceAmount = new Dictionary<ResourceTypeSO, int>();
        resourceUsage = new Dictionary<ResourceTypeSO, int>();
        resourceGet = new Dictionary<ResourceTypeSO, int>();
        currentMaximumResource = new Dictionary<ResourceTypeSO, int>();
        maximumResource = new Dictionary<ResourceTypeSO, int>();
        maximumPossibleResource = new Dictionary<ResourceTypeSO, int>();

        int limit = resourceTypes.Length;
        int i = 0;

        foreach (ResourceTypeSO resourceType in resourceTypes)
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
        resourceRefreshTime = resourceRefreshTimeMax;
        int[] newResourceAmounts = new int[resourceAmount.Count];

        for(int i = 0; i < resourceTypes.Length; i++)
        {
            int currentResourceAmount = resourceUsage[resourceTypes[i]];
            SubstractResourceAmount(resourceTypes[i], currentResourceAmount);

            currentResourceAmount = resourceGet[resourceTypes[i]];
            AddResourceAmount(resourceTypes[i], currentResourceAmount);

            newResourceAmounts[i] = resourceAmount[resourceTypes[i]];
        }

        OnResourceAmountChange?.Invoke(resourceTypes, newResourceAmounts);
    }
    public void AddResourceAmount(ResourceTypeSO resourceType, int amount)
    {
        resourceAmount[resourceType] += amount;
    }
    public void SubstractResourceAmount(ResourceTypeSO resourceType, int amount)
    {
        resourceAmount[resourceType] -= amount;
    }
    public void SubstractResourceAmountAll(int amount)
    {
        int[] newResourceAmounts = new int[resourceAmount.Count];

        for (int i = 0; i < resourceTypes.Length; i++)
        {
            SubstractResourceAmount(resourceTypes[i], amount);;
            newResourceAmounts[i] = resourceAmount[resourceTypes[i]];
        }

        OnResourceAmountChange?.Invoke(resourceTypes, newResourceAmounts);
    }
    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return resourceAmount[resourceType];
    }
    public string[] GetResourceNames()
    {
        string[] resourceNames = new string[3];
        int resourceNumber = 0;

        foreach (ResourceTypeSO resourceType in resourceTypes)
        {
            resourceNames[resourceNumber] = resourceType.resourceName;
            resourceNumber++;
        }

        return resourceNames;
    }
    public int GetResourceCurrentMaximumAmount(ResourceTypeSO resourceType)
    {
        return currentMaximumResource[resourceType];
    }
    // elérhető maximum
    public int GetMaximumResourceAmount(ResourceTypeSO resourceType)
    {
        return maximumPossibleResource[resourceType];
    }
    public int[] GetMaximumResourceAmounts()
    {
        var _resourceAmount = new int[3];
        for (int j = 0; j < maximumResourceCapacityLimit.Count; j++)
        {
            _resourceAmount[j] = maximumResourceCapacityLimit[j];
        }

        return _resourceAmount;

    }
    //Jelenlegi maximum
    public int GetCurrentMaximumResourceAmount(ResourceTypeSO resourceType)
    {
        return maximumResource[resourceType];
    }
    public int[] GetCurrentMaximumResourceAmounts()
    {
        var currentResourceAmount = new int[3];
        var i = 0;
        foreach (var items in currentMaximumResourceCapacity)
        {
            currentResourceAmount[i] = items;
            i++;
        }

        return currentResourceAmount;
    }
    //jelenlegi resource
    public int GetResoruceAmount(ResourceTypeSO resourceType)
    {
        return resourceAmount[resourceType];
    }
    public int[] GetResourceAmounts()
    {
        int[] resourceAmounts = new int[3];
        int resourceNumber = 0;

        foreach (ResourceTypeSO resourceType in resourceTypes)
        {
            resourceAmounts[resourceNumber] = resourceAmount[resourceType];
            resourceNumber++;
        }

        return resourceAmounts;
    }
}
