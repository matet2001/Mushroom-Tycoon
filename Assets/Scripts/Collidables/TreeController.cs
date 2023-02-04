using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : PlantBase
{
    [SerializeField] TreeTypeSO treeType;

    private ResourceTypeSO[] resourceTypes = new ResourceTypeSO[3];

    private Dictionary<ResourceTypeSO, float> resourceAmount;
    private Dictionary<ResourceTypeSO, float> resourceUsage;
    private Dictionary<ResourceTypeSO, float> resourceGet;
    private Dictionary<ResourceTypeSO, float> resourceAdd;
    private Dictionary<ResourceTypeSO, float> maxResourceAmount;

    private int growthNumber = 0;
    private float growTime;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        GetComponents();
        SetUpTreeType();       
    }
    private void GetComponents()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void SetUpTreeType()
    {
        growTime = treeType.growTimeArray[0];
        spriteRenderer.sprite = treeType.growthSprites[0];

        SetUpResources();
    }  
    private void SetUpResources()
    {
        resourceTypes = Resources.Load<ResourceTypeContainer>("ResourceTypeContainer").resourceTypeArray;

        resourceAmount = new Dictionary<ResourceTypeSO, float>();
        resourceUsage = new Dictionary<ResourceTypeSO, float>();
        resourceGet = new Dictionary<ResourceTypeSO, float>();
        resourceAdd = new Dictionary<ResourceTypeSO, float>();
        maxResourceAmount = new Dictionary<ResourceTypeSO, float>();

        for (int i = 0; i < treeType.resourceUsageArray.Length; i++)
        {
            resourceAmount.Add(resourceTypes[i], 50);
            resourceUsage.Add(resourceTypes[i], treeType.resourceUsageArray[i]);
            resourceAdd.Add(resourceTypes[i], treeType.resourceAddArray[i]);
            resourceGet.Add(resourceTypes[i], 5);
            maxResourceAmount.Add(resourceTypes[i], 100);
        }
    }
    public override void Collision()
    {
        YarnConnectionController.Instance.AddToTreeControllerList(this);
    }
    private void Update()
    {
        CountDownGrow();
    }
    private void CountDownGrow()
    {
        if(growTime > 0) 
        {
            growTime -= Time.deltaTime;
        }
        else
        {
            NextGrowState();
        }
    }
    private void NextGrowState()
    {
        growthNumber++;
        growTime = treeType.growTimeArray[growthNumber];
        spriteRenderer.sprite = treeType.growthSprites[growthNumber];
    }
}
