using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TreeTypeSO", menuName = "ScriptableObjects/TreeType")]
public class TreeTypeSO : ScriptableObject
{
    public string treeName;

    public float[] resourceAmount = new float[3];
    [HideInInspector]
    public float[] resourceUsage = new float[3];
    public float[] resourceUse = new float[3];
    public float[] resourceProduce = new float[3];
    public float[] resourceMax = new float[3];

    public int growTime;
    public Sprite[] treeSprites;
}
