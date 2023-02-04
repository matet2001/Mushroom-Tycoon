using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public static GameStateController Instance;
    public GameStateControllerDataContainer gameStateControllerDataContainer { private set; get; }

    public event Action OnManagementStateEnter;
    public event Action OnConquerStateEnter;

    [SerializeField] GameState managerState;

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
            DontDestroyOnLoad(gameObject);
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
    public void ChangeToManagerState()
    {
        gameStateControllerDataContainer.stateMachine.ChangeState(managerState);
    }
}
public class GameStateControllerDataContainer
{
    public GameStateController gameStateController;
    public StateMachine stateMachine;
}
