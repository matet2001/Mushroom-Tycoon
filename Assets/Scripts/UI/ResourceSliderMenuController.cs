using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSliderMenuController : ResourcesSliderWorldController
{
    [SerializeField] Transform plusIconTransform;
    [SerializeField] Transform minusIconTransform;

    [SerializeField] float distanceToClick = 0.2f;

    public bool IsMouseCloseToPlus()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        return Vector2.Distance(mousePosition, plusIconTransform.position) < distanceToClick;
    }
    public bool IsMouseCloseToMinus()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        return Vector2.Distance(mousePosition, minusIconTransform.position) < distanceToClick;
    }
}