using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject Projectile;
    public GameObject Thrower;
    private float acc;
    public float Spos;
    public float Epos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) )
        {
            Spos = Input.mousePosition.y;
        }
        else if (Input.GetMouseButton(0))
        {
            Epos = Input.mousePosition.y;
            //   acc = -((Spos - Epos) % 100) / 5; ;
            acc = Spos - Epos;
            Debug.Log(acc);
            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Rigidbody rg = Instantiate(Projectile, new Vector3(Thrower.transform.position.x, Thrower.transform.position.y, Thrower.transform.position.z), Quaternion.identity).GetComponent<Rigidbody>();
            rg.AddForce(acc, 0, 0, ForceMode.Impulse);
        }

        
    }
}
