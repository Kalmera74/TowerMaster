using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPhase", menuName = "New Phase")]
public class Phase : ScriptableObject
{
    public GameObject[] Enemies;
}
