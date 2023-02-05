using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManagement : GameState
{
    public GameObject managerVirtualCamera;
    
    public override void OnEnter()
    {
        if (Time.timeSinceLevelLoad < 30) return;
        
        gameStateControllerDataContainer.gameStateController.FireOnManagmentStateEnter();
        base.OnEnter();
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            return true;
        }
        return false;
    }
}
