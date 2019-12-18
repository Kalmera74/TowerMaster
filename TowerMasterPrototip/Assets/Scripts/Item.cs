using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Material _skin;
    private int _cost;

    public void ApplySkin(MeshRenderer skin) {
        skin.material = _skin;
    }
}
