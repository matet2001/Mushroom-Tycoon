using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TreeTypeSO", menuName = "ScriptableObjects/TreeType")]
public class TreeTypeSO : ScriptableObject
{
    public string treeName;
    public float[] resourceAmount, resourceUsage, resourceGet, resourceAdd, resourceMax;

    public int growthTime;
    public Sprite[] treeSprites;
}
