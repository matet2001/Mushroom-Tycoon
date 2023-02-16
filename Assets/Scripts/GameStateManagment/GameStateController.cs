using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    public static GameStateController Instance;
    public GameStateControllerDataContainer gameStateControllerDataContainer { private set; get; }

    public event Action OnManagementStateEnter;
    public event Action OnConquerStateEnter;
    public event Action OnDeadStateEnter;

    [SerializeField] GameState managerState, deadState;

    private TreeController currentUIShowTree;

    private void Awake()
    {
        SingletonPattern();
        SetUpGameStateControllerDataContainer();
    }
    private void SingletonPattern()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else
        {
            Instance = this;
        }
    }
    private void SetUpGameStateControllerDataContainer()
    {
        gameStateControllerDataContainer = new GameStateControllerDataContainer();

        gameStateControllerDataContainer.gameStateController = this;
        gameStateControllerDataContainer.stateMachine = GetComponentInChildren<StateMachine>();
    }
    public void FireOnManagmentStateEnter()
    {
        OnManagementStateEnter?.Invoke();
    }
    public void FireOnConquerStateEnter() 
    {
        OnConquerStateEnter?.Invoke(); 
    }
    public void FireOnDeadStateEnter()
    {
        OnDeadStateEnter?.Invoke();
    }
    public void ChangeToManagerState()
    {
        gameStateControllerDataContainer.stateMachine.ChangeState(managerState);
    }
    public void ChangeToDeadState()
    {
        gameStateControllerDataContainer.stateMachine.ChangeState(deadState);
    }
    public bool CanShowUI()
    {
        if (gameStateControllerDataContainer.stateMachine.currentState == managerState)
        {
            return true;
        }
        return false;
    }
    public bool WannaShowUI(TreeController treeController)
    {
        if(treeController == currentUIShowTree)
        {
            return true;
        }
        if(!currentUIShowTree)
        {
            currentUIShowTree = treeController;
            return true;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        float distanceFromCurrentTree = Vector2.Distance(mousePos, treeController.transform.position);
        float distanceOldCurrentTree = Vector2.Distance(mousePos, currentUIShowTree.transform.position);

        if(distanceFromCurrentTree < distanceOldCurrentTree)
        {
            currentUIShowTree = treeController;
            return true;
        }
        return false;
    }
}
public class GameStateControllerDataContainer
{
    public GameStateController gameStateController;
    public StateMachine stateMachine;
}
