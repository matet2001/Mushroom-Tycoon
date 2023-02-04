using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YarnConnectionController : MonoBehaviour
{
    public static YarnConnectionController Instance;

    public List<TreeController> connectedTreeControllerList = new List<TreeController>();

    private void Awake()
    {
        SingletonPattern();
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
    public void AddToTreeControllerList(TreeController treeController)
    {
        connectedTreeControllerList.Add(treeController);
    }
}
