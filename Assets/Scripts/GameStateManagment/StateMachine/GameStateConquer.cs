using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateConquer : GameState
{
    public GameObject conquererVirtualCamera;
    public TutorialManager tutorialManager;
    
    public override void OnEnter()
    {
        StartCoroutine(ResourceManager.Instance.connectionManager.yarn.spawnProtection());
        tutorialManager.hasSeenTutorial = true;
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
        if (Input.GetMouseButtonDown(1))
        {
            return true;
        }
        return false;
    }
}
