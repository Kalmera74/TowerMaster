using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{

    protected override void Move()
    {
        base.Move();
    }
    private void changeColor()
    {

    }

    protected override void Die()
    {
        base.Die();
        GameManager.Next = true;
    }
}
