using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateConquer : GameState
{
    public GameObject conquererVirtualCamera;
    
    public override void OnEnter()
    {
    }
    public override void OnUpdate()
    {
        
    }
    public override void OnExit()
    {
        conquererVirtualCamera.SetActive(false);
        base.OnExit();
    }
    public override bool TransitionToThisState()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            return true;
        }
        return false;
    }
}
