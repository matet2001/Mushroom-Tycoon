using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    public GameObject conquererStateVirtualCamera;
    public GameObject managementStateVirtualCamera;

    private void Awake()
    {
        SingletonPattern();
        PrefaceInstanceCameraSelect();
    }

    private void Update()
    {
        SwapCameraState();
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

    private void SwapCameraState()
    {
        GameStateController.Instance.OnConquerStateEnter += SwapCameraToConquer;
        GameStateController.Instance.OnManagementStateEnter += SwapCameraToManagement;
    }
    
    private void SwapCameraToConquer()
    {
        managementStateVirtualCamera.SetActive(false);
        conquererStateVirtualCamera.SetActive(true);
    }
    
    private void SwapCameraToManagement()
    {
        managementStateVirtualCamera.SetActive(true);
        conquererStateVirtualCamera.SetActive(false);
    }

    private void PrefaceInstanceCameraSelect()
    {
        managementStateVirtualCamera = GameObject.FindGameObjectWithTag("ManagementCamera");
        conquererStateVirtualCamera = GameObject.FindGameObjectWithTag("ConquerCamera");
        SwapCameraToManagement();
    }
}
