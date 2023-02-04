using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    public List<TreeController> treeControllerList;

    public void AddToTreeControllerList(TreeController treeController)
    {
        treeControllerList.Add(treeController);
    }
}
