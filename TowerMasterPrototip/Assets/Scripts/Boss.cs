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
        StartCoroutine(RandMove(10, 3.5f));
        Skin = GameManager.GetRandomEnemyColor(0, 4);
        transform.Rotate(0, 90, 0);
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

        /* transform.position = new Vector3(transform.position.x, transform.position.y, (Vector3.left + Random.insideUnitSphere * 3).z);
         yield return new WaitForSeconds(wait);
         Debug.Log(times);*/

        
            Skin = GameManager.GetRandomEnemyColor(0, 4);
        yield return new WaitForSeconds(wait);
        StartCoroutine(RandMove(0,wait));
        
    }
    protected override void Die()
    {
        base.Die();
        GameManager.Next = true;
    }
}
