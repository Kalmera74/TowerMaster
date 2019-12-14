using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject Projectile;
    public GameObject Thrower;
    public float acc;
    public float side;
    public float SposY;
    public float EposY;
    public float SposX;
    public float EposX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) )
        {
            SposY = Input.mousePosition.y;
            SposX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            EposY = Input.mousePosition.y;
            EposX = Input.mousePosition.x;
            acc = ((SposY - EposY) /100) *10 ;
            side=-(((SposX-EposX)/100)*1.5f);
            //acc = Spos - Epos;




            

        }
        else if (Input.GetMouseButtonUp(0))
        {
           

            Rigidbody rg = Instantiate(Projectile, new Vector3(Thrower.transform.position.x, Thrower.transform.position.y, Thrower.transform.position.z), Quaternion.identity).GetComponent<Rigidbody>();
            rg.AddForce(10, acc, side, ForceMode.Impulse);
        }

        
    }
}
