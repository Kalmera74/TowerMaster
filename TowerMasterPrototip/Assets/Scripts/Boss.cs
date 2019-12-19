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
        Skin = GameManager.GetRandomEnemyColor(0, 3);
    }
    private void Start()
    {
        StartCoroutine(RandMove(10, 1.5f));
    }
    private void Update()
    {
        Move();
        if (Input.anyKeyDown)
        {
            //   transform.position =new Vector3(transform.position.x,transform.position.y, (Vector3.left + Random.insideUnitSphere*3).z);

          //  changeColor();

        }
    }

    // Not perfect 
    private IEnumerator RandMove(int times, float wait)
    {
        if (times > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, (Vector3.left + Random.insideUnitSphere * 3).z);
            yield return new WaitForSeconds(wait);
            Debug.Log(times);
            StartCoroutine(RandMove(times - 1,wait));
        }
    }
    protected override void Die()
    {
        base.Die();
        GameManager.Next = true;
    }
}
