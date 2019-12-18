using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewWave",menuName ="New Wave")]
public class Wave : ScriptableObject
{
    public GameObject[] FirstRow;
    public GameObject[] SecondRow;
    public GameObject[] ThirdRow;
    public GameObject Boss;
    
}
