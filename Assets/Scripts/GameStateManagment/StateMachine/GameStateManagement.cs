using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManagement : GameState
{
    public GameObject managerVirtualCamera;
    
    public override void OnEnter()
    {
        base.OnEnter();
        gameStateControllerDataContainer.gameStateController.FireOnManagmentStateEnter();
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
