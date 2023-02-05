using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreeController : PlantBase
{
    public ResourceData resourceData;
    [SerializeField] TreeTypeSO treeType;
    
    private SpriteRenderer spriteRenderer;

    [SerializeField] GameObject selectedUI, resourceValuesUI, treeMenusUI;

    private int growTime, growLevel;

    private bool shouldDisplayUI;
    [SerializeField] float showDistance = 4f;
 
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
        spriteRenderer.sprite = treeType.treeSprites[0];
        growTime = treeType.growTime;
        SetUpResources();
    }
    private void SetUpResources()
    {
        resourceData = new ResourceData(Resources.Load<ResourceTypeContainer>("ResourceTypeContainer"), treeType.resourceAmount, treeType.resourceUsage, treeType.resourceGet, treeType.resourceProduce, treeType.resourceMax);
    }
    private void Start()
    {
        ResourceManager.Instance.OnResourceAmountRefresh += Instance_OnResourceAmountRefresh;
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
        if (selectedUI.activeSelf != shouldDisplayUI) selectedUI.SetActive(shouldDisplayUI);
        if (resourceValuesUI.activeSelf != shouldDisplayUI) resourceValuesUI.SetActive(shouldDisplayUI);
        if (treeMenusUI.activeSelf != shouldDisplayUI) treeMenusUI.SetActive(shouldDisplayUI);

        if (!shouldDisplayUI) return;

        selectedUI.transform.Rotate(0f, 0f, 50f * Time.deltaTime, Space.Self);
    }
    public override void Collision()
    {
        ResourceManager.Instance.connectionManager.AddToTreeControllerList(this);
    }
    private void Instance_OnResourceAmountRefresh()
    {
        GrowCounter();
        RefreshResourceAmounts();
    }
    private void GrowCounter()
    {
        growTime--;

        if (growTime <= 0)
        {
            growLevel++;
            spriteRenderer.sprite = treeType.treeSprites[growLevel];
            growTime = treeType.growTime * growLevel;

        }
    }
    private void RefreshResourceAmounts()
    {
        int number = 0;

        foreach (ResourceTypeSO resourceType in resourceData.resourceTypes)
        {
            float resourceProduce = resourceData.resourceProduce[resourceType];
            float resourceGet = resourceData.resourceUse[resourceType];

            resourceData.resourceUsage[resourceType] = treeType.resourceUsage[number] * growLevel + resourceProduce + resourceGet;
            resourceData.resourceMax[resourceType] += treeType.resourceMax[number] * growLevel;

            resourceData.resourceAmount[resourceType] += resourceData.resourceUsage[resourceType];

            number++;
        }
    }
}
