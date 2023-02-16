using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateDead : GameState
{   
    public override void OnEnter()
    {
        gameStateControllerDataContainer.gameStateController.FireOnDeadStateEnter();
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
        return false;
    }
}
