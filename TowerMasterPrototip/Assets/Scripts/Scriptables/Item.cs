using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "New Item")]
public class Item : ScriptableObject
{
    public Texture _skin;
    public int _cost;

}
