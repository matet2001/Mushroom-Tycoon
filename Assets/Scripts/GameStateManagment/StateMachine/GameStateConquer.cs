using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateConquer : GameState
{
    public GameObject conquererVirtualCamera;
    
    public override void OnEnter()
    {
        StartCoroutine(ResourceManager.Instance.connectionManager.yarn.spawnProtection());
        gameStateControllerDataContainer.gameStateController.FireOnConquerStateEnter();
    }
    public override void OnUpdate()
    {
        
    }
    public override void OnExit()
    {
        base.OnExit();
    }
    public override bool TransitionToThisState()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            return true;
        }
        return false;
    }
}
