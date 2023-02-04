using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    private ResourceTypeSO[] resourceTypes = new ResourceTypeSO[3];

    private Dictionary<ResourceTypeSO, float> resourceAmount;
    private Dictionary<ResourceTypeSO, float> resourceUsage;
    private Dictionary<ResourceTypeSO, float> resourceGet;
    private Dictionary<ResourceTypeSO, float> maxResourceAmount;

    public event Action<ResourceTypeSO[], float[]> OnResourceAmountChange;

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
        resourceTypes = Resources.Load<ResourceTypeContainer>("ResourceTypeContainer").resourceTypeArray;

        resourceAmount = new Dictionary<ResourceTypeSO, float>();
        resourceUsage = new Dictionary<ResourceTypeSO, float>();
        resourceGet = new Dictionary<ResourceTypeSO, float>();
        maxResourceAmount = new Dictionary<ResourceTypeSO, float>();

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
        float[] newResourceAmounts = new float[resourceAmount.Count];

        for(int i = 0; i < resourceTypes.Length; i++)
        {
            float currentResourceAmount = resourceUsage[resourceTypes[i]];
            SubstractResourceAmount(resourceTypes[i], currentResourceAmount);

            currentResourceAmount = resourceGet[resourceTypes[i]];
            AddResourceAmount(resourceTypes[i], currentResourceAmount);

            newResourceAmounts[i] = resourceAmount[resourceTypes[i]];
        }

        OnResourceAmountChange?.Invoke(resourceTypes, newResourceAmounts);
    }
    public void AddResourceAmount(ResourceTypeSO resourceType, float amount)
    {
        resourceAmount[resourceType] += amount;
    }
    public void SubstractResourceAmount(ResourceTypeSO resourceType, float amount)
    {
        resourceAmount[resourceType] -= amount;
    }
    public void SubstractResourceAmountAll(float amount)
    {
        float[] newResourceAmounts = new float[resourceAmount.Count];

        for (int i = 0; i < resourceTypes.Length; i++)
        {
            SubstractResourceAmount(resourceTypes[i], amount);;
            newResourceAmounts[i] = resourceAmount[resourceTypes[i]];
        }

        OnResourceAmountChange?.Invoke(resourceTypes, newResourceAmounts);
    }
    public float GetResourceAmount(ResourceTypeSO resourceType)
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
    public float[] GetResourceAmounts()
    {
        float[] resourceAmounts = new float[3];
        int resourceNumber = 0;

        foreach (ResourceTypeSO resourceType in resourceTypes)
        {
            resourceAmounts[resourceNumber] = resourceAmount[resourceType];
            resourceNumber++;
        }

        return resourceAmounts;
    }
}
