using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject Projectile;
    public GameObject Thrower;


    public Rigidbody ball;
	public Transform target;
	public float h = 25;
	public float gravity = -18;
    public float TargetSens = 4;
    
    GameObject[] PathObjects = new GameObject[10]; 
    LaunchData launchData;
    Vector3 TargetStartPos;
    Vector3 TargetBufferPos;

    void Start()
    {
        Physics.gravity = Vector3.up * gravity;
        TargetStartPos = target.transform.localPosition;
        TargetBufferPos = target.transform.localPosition;
        for (int i = 0; i < PathObjects.Length; i++) {
            PathObjects[i] = Instantiate(target.gameObject);
            PathObjects[i].SetActive(false);
        }
    }

    void SetVisible() {
        for (int i = 0; i < PathObjects.Length; i++) {
            PathObjects[i].SetActive(false);
        }
    }

    void Update()
    {
        target.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        
        if (Input.GetMouseButton(0))
        {
            Vector3 inputDirection = Vector3.zero * 0.2f;
            inputDirection.x = -Input.GetAxisRaw("Mouse Y") * 0.1f;
            inputDirection.z = Input.GetAxisRaw("Mouse X") * 0.1f;
            target.transform.localPosition = TargetBufferPos + inputDirection * TargetSens;
            TargetBufferPos = target.transform.localPosition;
            DrawPath();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            SetVisible();
            target.transform.position = TargetStartPos;
            TargetBufferPos = TargetStartPos;
            Rigidbody rg = Instantiate(Projectile, Thrower.transform.position, Quaternion.Euler(Camera.main.transform.rotation.eulerAngles)).GetComponent<Rigidbody>();
            rg.velocity = launchData.initialVelocity;
        }
    }

    LaunchData CalculateLaunchData() {
		float displacementY = target.position.y - ball.position.y;
		Vector3 displacementXZ = new Vector3 (target.position.x - ball.position.x, 0, target.position.z - ball.position.z);
		float time = Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementY - h)/gravity);
		Vector3 velocityY = Vector3.up * Mathf.Sqrt (-2 * gravity * h);
		Vector3 velocityXZ = displacementXZ / time;

		return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
	}

	void DrawPath() {
		launchData = CalculateLaunchData ();
		Vector3 previousDrawPoint = ball.position;

		int resolution = 10;
		for (int i = 1; i <= resolution; i++) {
			float simulationTime = i / (float)resolution * launchData.timeToTarget;
			Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up *gravity * simulationTime * simulationTime / 2f;
			Vector3 drawPoint = ball.position + displacement;
            PathObjects[i - 1].transform.position = previousDrawPoint;
            PathObjects[i - 1].SetActive(true);
			previousDrawPoint = drawPoint;
		}
	}

	struct LaunchData {
		public readonly Vector3 initialVelocity;
		public readonly float timeToTarget;

		public LaunchData (Vector3 initialVelocity, float timeToTarget)
		{
			this.initialVelocity = initialVelocity;
			this.timeToTarget = timeToTarget;
		}
		
	}
}
