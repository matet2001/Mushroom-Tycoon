using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YarnCrosshair : MonoBehaviour
{
    public Transform crosshairObject;

    [HideInInspector]
    public YarnController yarnController;
    
    
    void Start()
    {
        yarnController = GetComponent<YarnController>();
    }
    
    void Update()
    {
        crosshairObject.gameObject.SetActive(yarnController.GetMoveStatus());
        
        if (!yarnController.GetMoveStatus()) return;
            crosshairObject.transform.localPosition = yarnController.GetDirection().normalized * 1.5f;
    }
}
