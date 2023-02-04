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
    private Dictionary<ResourceTypeSO, int> maxResourceAmount;

    public event Action<ResourceTypeSO[], int[]> OnResourceAmountChange;

    [SerializeField] float resourceRefreshTime = 3;
    private float resourceRefreshTimeMax;

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
        maxResourceAmount = new Dictionary<ResourceTypeSO, int>();

        foreach (ResourceTypeSO resourceType in resourceTypes)
        {
            
            resourceAmount.Add(resourceType, 50);
            resourceUsage.Add(resourceType, 5);
            resourceGet.Add(resourceType, 3);
            maxResourceAmount.Add(resourceType, 100);
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
