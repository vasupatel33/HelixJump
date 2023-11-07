using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LocalData",menuName ="Scriptable",order =5)]
public class Demo : ScriptableObject
{
    public string Name;
    public int Age;
    public float[] Per;
}
