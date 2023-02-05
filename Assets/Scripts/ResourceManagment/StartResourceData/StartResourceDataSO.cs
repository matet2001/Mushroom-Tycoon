using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StarterResourceDataSO", menuName = "ScriptableObjects/StarterResourceData")]
public class StartResourceDataSO : ScriptableObject
{
    public float[] resourceAmount = new float[3];
    public float[] resourceUsage = new float[3];
    public float[] resourceUse = new float[3];
    public float[] resourceProduce = new float[3];
    public float[] resourceMax = new float[3];
}
