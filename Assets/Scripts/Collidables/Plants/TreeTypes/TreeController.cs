using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class TreeController : PlantBase
{
    
    public ResourceData resourceData;
    [SerializeField] TreeTypeSO treeType;
    [Space(5f), Header("Selected sprite attributes")]
    public bool isSelected;

    [Space, Range(1,35f)] public float rotationSpeed;
    [SerializeField] private Sprite _selectedObjectSprite; //Ez vagy null
    [SerializeField] private SpriteRenderer selectedSpriteRenderer;
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

    private void Update()
    {
        SetSprite(isSelected, _selectedObjectSprite);
        switch (isSelected)
        {
            case true:
                RotateSelectorSprite(rotationSpeed);
                break;
            case false:
                selectedSpriteRenderer.transform.localEulerAngles = Vector3.zero;
                break;
        }
    }

    private void SetSprite(bool isSelected, Sprite selectedSprite)
        => selectedSpriteRenderer.sprite = isSelected 
            ? selectedSprite 
            : null;

    private void RotateSelectorSprite(float degree)
        => selectedSpriteRenderer.gameObject.transform.localEulerAngles =
            new Vector3(0, 0, selectedSpriteRenderer.transform.localEulerAngles.z + degree * Time.deltaTime);
    
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
