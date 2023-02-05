using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ResourceTypeSO", menuName = "ScriptableObjects/ResourceType")]
public class ResourceTypeSO : ScriptableObject
{
    public string resourceName;
    public Sprite resourceImageUI;
}

