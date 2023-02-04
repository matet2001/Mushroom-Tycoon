using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class TreeController : PlantBase
{
    
    public ResourceData resourceData;
    [SerializeField] TreeTypeSO treeType;
    
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer selectedUISpriteRenderer;
    [SerializeField] GameObject selectedUI;

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
        selectedUISpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
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

        if (distance <= showDistance)
        {
            shouldDisplayUI = true;
        }
        else
        {
            shouldDisplayUI = false;
        }
    }
    private void DisplayUI()
    {
        if (selectedUI.activeSelf != shouldDisplayUI) selectedUI.SetActive(shouldDisplayUI);

        if (!shouldDisplayUI) return;

        selectedUI.transform.Rotate(0f, 0f, 50f * Time.deltaTime, Space.Self);
    }
    private void SetUpTreeType()
    {
        spriteRenderer.sprite = treeType.treeSprites[0];
        growTime = treeType.growTime;
        SetUpResources();
    }
    private void SetUpResources()
    {
        resourceData = new ResourceData(Resources.Load<ResourceTypeContainer>("ResourceTypeContainer"), treeType.resourceAmount, treeType.resourceUsage, treeType.resourceGet, treeType.resourceAdd, treeType.resourceMax);
    }
    public override void Collision()
    {
        ResourceManager.Instance.connectionManager.AddToTreeControllerList(this);
    }
    private void Instance_OnResourceAmountRefresh()
    {
        growTime--;

        if(growTime <= 0)
        {
            growLevel++;
            spriteRenderer.sprite = treeType.treeSprites[growLevel];
            growTime = treeType.growTime * growLevel;
        }
    }
}
