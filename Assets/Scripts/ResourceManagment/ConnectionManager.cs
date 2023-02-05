using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    public List<TreeController> treeControllerList;
    public List<MushroomController> mushroomControllerList;
    
    private void Awake()
    {
        treeControllerList = new List<TreeController>();
        mushroomControllerList = new List<MushroomController>();
    }

    public void AddToTreeControllerList(TreeController treeController)
    {
        if (treeControllerList.Contains(treeController)) return;
        
        treeControllerList.Add(treeController);
    }
    public void RemoveFromTreeControllerList(TreeController treeController)
    {
        if (!treeControllerList.Contains(treeController)) return;

        treeControllerList.Remove(treeController);
    }
    public void AddToMushroomControllerList(MushroomController mushroomController)
    {
        if (mushroomControllerList.Contains(mushroomController)) return;

        mushroomControllerList.Add(mushroomController);
    }
}
