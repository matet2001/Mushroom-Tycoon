using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporeRootInstance : MonoBehaviour
{
    private Vector3 target;
    private Vector3 startingPosition;
    public float interp;

    public void SetGoal(Vector3 startPos, Vector3 endPos)
    {
        target = endPos;
        startingPosition = startPos;
    }
    
    private void Update()
    {
        transform.position = Vector3.Lerp(startingPosition, target, interp);
        if (interp < 1)
            interp += Time.deltaTime;
        else
        {
            interp = 1;
            GetComponent<SporeRootInstance>().enabled = false;
        }
    }
}
