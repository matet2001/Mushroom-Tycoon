using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : PlantBase
{
    [SerializeField] StartResourceDataSO startResourceData;
    public ResourceData resourceData { get; private set; }

    private void Awake()
    {
        resourceData = new ResourceData(Resources.Load<ResourceTypeContainer>("ResourceTypeContainer"), startResourceData);
    }

    private void Start()
    {
        ResourceManager.Instance.connectionManager.AddToMushroomControllerList(this);
    }

    public override void Collision()
    {
        
    }
}
