using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{

    public override void Move()
    {
        transform.position += new Vector3(_moveSpeed * Time.deltaTime, 0, 0);
    }
    private void changeColor()
    {

    }
}
