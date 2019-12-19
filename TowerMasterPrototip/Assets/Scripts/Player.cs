using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _goldAmount;
    [SerializeField]
    private Material _skin;

    public void AddGold(int amount)
    {
        _goldAmount += amount;
    }
    public void SubstructGold(int amount)
    {
        _goldAmount -= amount;
    }

    public Material Skin
    {
        get { return _skin; }
        set
        {
            _skin = value;
            GetComponent<MeshRenderer>().material = _skin;
        }
    }

    public int GoldAmount { get => _goldAmount;  }
}
