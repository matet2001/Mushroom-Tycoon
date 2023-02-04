using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManagement : GameState
{
    public GameObject managerVirtualCamera;
    
    public override void OnEnter()
    {
        base.OnEnter();
        print("manager enter");
    }
    public override void OnUpdate()
    {
        
    }
    public override void OnExit()
    {
        print("manager exit");
        base.OnExit();
    }
    public override bool TransitionToThisState()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            return true;
        }
        return false;
    }
}
