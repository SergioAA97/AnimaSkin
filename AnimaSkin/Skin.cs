using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

[System.Serializable]
public class Skin : MonoBehaviour
{
    [SerializeField]
    public string skinName;
    [HideInInspector]
    [SerializeField]
    public List<SkinPart> skinParts;
}

