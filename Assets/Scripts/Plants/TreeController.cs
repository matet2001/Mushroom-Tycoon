using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : PlantBase
{
    
    public ResourceData resourceData;
    [SerializeField] TreeTypeSO treeType;

    private SpriteRenderer spriteRenderer;

    private int growTime, growLevel;
 
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetUpTreeType();
        
    }
    private void Start()
    {
        ResourceManager.Instance.OnResourceAmountRefresh += Instance_OnResourceAmountRefresh;
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
