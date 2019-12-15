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
            side=-(((SposX-EposX)/100)*2.0f);
            //acc = Spos - Epos;




            

        }
        else if (Input.GetMouseButtonUp(0))
        {
           

            Rigidbody rg = Instantiate(Projectile, Thrower.transform.position, Quaternion.Euler(Camera.main.transform.rotation.eulerAngles)).GetComponent<Rigidbody>();
            rg.AddForce(10, acc, side, ForceMode.Impulse);
        }

        
    }
}
