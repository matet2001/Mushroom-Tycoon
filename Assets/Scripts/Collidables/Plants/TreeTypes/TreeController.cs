using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreeController : PlantBase
{
    public ResourceData resourceData { get; private set;}
    [SerializeField] TreeTypeSO treeType;
    
    private SpriteRenderer spriteRenderer;
    private new Collider2D collider;

    [SerializeField] GameObject selectedUI, resourceValuesUI, treeMenusUI;
    TreeMenuController treeMenuController;

    private int growTime, growLevel = 1;
    public Dictionary<ResourceTypeSO, float> resourceTradeAmount;

    private bool shouldDisplayUI = true;
    private bool isConnected = false;
    [SerializeField] float showDistance = 4f;
 
    private void Awake()
    {
        GetComponents();
        SetUpTreeType();
        InitResourceTradeAmount();
    }  
    private void GetComponents()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }
    private void SetUpTreeType()
    {
        spriteRenderer.sprite = treeType.treeSprites[0];
        ResetCollider();
        growTime = treeType.growTime;
        SetUpResources();
    }

    private void ResetCollider()
    {
        Destroy(collider);
        collider = gameObject.AddComponent<PolygonCollider2D>();
        collider.isTrigger = true;
    }

    private void SetUpResources()
    {
        resourceData = new ResourceData(treeType);
    }
    private void InitResourceTradeAmount()
    {
        resourceTradeAmount = new Dictionary<ResourceTypeSO, float>();
        foreach (ResourceTypeSO resourceType in resourceData.resourceTypes)
        {
            resourceTradeAmount.Add(resourceType, 0f);
        }
    }
    private void Start()
    {
        ResourceManager.Instance.OnResourceAmountRefresh += Instance_OnResourceAmountRefresh;
        treeMenuController = treeMenusUI.GetComponent<TreeMenuController>();
    }
    private void Update()
    {
        CheckShouldDisplayUI();
        DisplayUI();
    }
    private void CheckShouldDisplayUI()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(transform.position, mousePosition);

        if(distance > showDistance) shouldDisplayUI = false;
        else if (!GameStateController.Instance.CanShowUI()) shouldDisplayUI = false;
        else if(GameStateController.Instance.WannaShowUI(this)) shouldDisplayUI = true;
        else shouldDisplayUI = false;
    }
    private void DisplayUI()
    {
        if (!isConnected) return;

        if (selectedUI.activeSelf != shouldDisplayUI) selectedUI.SetActive(shouldDisplayUI);
        if (resourceValuesUI.activeSelf != shouldDisplayUI) resourceValuesUI.SetActive(shouldDisplayUI);
        if (treeMenusUI.activeSelf != shouldDisplayUI) treeMenusUI.SetActive(shouldDisplayUI);

        if (!shouldDisplayUI) return;

        selectedUI.transform.Rotate(0f, 0f, 50f * Time.deltaTime, Space.Self);
    }
    public override void Collision()
    {
        if (isConnected) return;
        
        ResourceManager.Instance.connectionManager.AddToTreeControllerList(this);
        isConnected = true;
    }
    private void Instance_OnResourceAmountRefresh()
    {
        if (isConnected) return;

        //GrowCounter();
        treeMenuController.UpdateResourceTradeValues();
        RefreshResourceAmounts();
    }
    private void GrowCounter()
    {
        growTime--;

        if (growTime <= 0)
        {
            growLevel++;
            spriteRenderer.sprite = treeType.treeSprites[growLevel - 1];
            growTime = treeType.growTime * growLevel;
            ResetCollider();
        }
    }
    private void RefreshResourceAmounts()
    {
        foreach (ResourceTypeSO resourceType in resourceData.resourceTypes)
        {
            CalculateResourceUsage(resourceType);

            resourceData.resourceAmount[resourceType] += resourceData.resourceUsage[resourceType];
            resourceData.resourceAmount[resourceType] += resourceTradeAmount[resourceType];
            resourceData.resourceAmount[resourceType] = Mathf.Min(resourceData.resourceAmount[resourceType], resourceData.resourceMax[resourceType]);
        }
    }
    private void CalculateResourceUsage(ResourceTypeSO resourceType)
    {
        float resourceProduce = resourceData.resourceProduce[resourceType];
        float resourceUse = resourceData.resourceUse[resourceType];

        resourceData.resourceUsage[resourceType] = (resourceProduce - resourceUse) * growLevel;
    }
    
}
