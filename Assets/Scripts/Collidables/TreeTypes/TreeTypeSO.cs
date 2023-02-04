using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TreeTypeSO", menuName = "ScriptableObjects/TreeType")]
public class TreeTypeSO : ScriptableObject
{
    public string treeTypeName;

    public float[] resourceUsageArray;
    public float[] resourceAddArray;

    public Sprite[] growthSprites;
    public float[] growTimeArray;
}
