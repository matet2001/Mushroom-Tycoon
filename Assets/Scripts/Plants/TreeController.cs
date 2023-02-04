using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : PlantBase
{
    public ResourceDataSO resourceData;
    [SerializeField] TreeTypeSO treeType;

    private SpriteRenderer spriteRenderer;
 
    private void Awake()
    {
        SetUpTreeType();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void SetUpTreeType()
    {
        spriteRenderer.sprite = treeType.treeSprites[0];
        SetUpResources();
    }
    private void SetUpResources()
    {
        resourceData = new ResourceDataSO(Resources.Load<ResourceTypeContainer>("ResourceTypeContainer"), treeType.resourceAmount, treeType.resourceUsage, treeType.resourceGet, treeType.resourceAdd, treeType.resourceMax);
    }
    public override void Collision()
    {

    }
}
