using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateConquer : GameState
{
    public override void OnEnter()
    {
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
        if (Input.GetKeyDown(KeyCode.D))
        {
            return true;
        }
        return false;
    }
}
