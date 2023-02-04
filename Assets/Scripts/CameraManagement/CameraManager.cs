using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
    [Range(1,50f)] public float cameraRotationSpeed;
    public static CameraManager Instance;
    [Space]
    public GameObject cameraParent;
    [Space]
    public GameObject conquererStateVirtualCamera;
    public GameObject managementStateVirtualCamera;
    [Space] public bool canMoveCamera;

    private void Awake()
    {
        SingletonPattern();
        PrefaceInstanceCameraSelect();
    }

    private void Update()
    {
        SwapCameraState();
        MoveCameraInManagementState();
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
    private void MoveCameraInManagementState()
    {
        float RotationDegree(float playerInput)
        {
            var current = cameraParent.transform.localEulerAngles.z;
            
            return current += (playerInput * Time.deltaTime * cameraRotationSpeed * -1f);
        }
        
        if (!canMoveCamera) return;
        
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        
        cameraParent.transform.localEulerAngles = new Vector3(0f, 0f, RotationDegree(horizontalInput));
    }
    private void SwapCameraState()
    {
        GameStateController.Instance.OnConquerStateEnter += SwapCameraToConquer;
        GameStateController.Instance.OnManagementStateEnter += SwapCameraToManagement;
    }
    private void SwapCameraToConquer()
    {
        canMoveCamera = false;
        managementStateVirtualCamera.SetActive(false);
        conquererStateVirtualCamera.SetActive(true);
    }
    private void SwapCameraToManagement()
    {
        canMoveCamera = true;
        managementStateVirtualCamera.SetActive(true);
        conquererStateVirtualCamera.SetActive(false);
    }
    private void PrefaceInstanceCameraSelect()
    {
        managementStateVirtualCamera = GameObject.FindGameObjectWithTag("ManagementCamera");
        conquererStateVirtualCamera = GameObject.FindGameObjectWithTag("ConquerCamera");
        cameraParent = GameObject.FindGameObjectWithTag("CameraParent");
        SwapCameraToManagement();
    }
}
