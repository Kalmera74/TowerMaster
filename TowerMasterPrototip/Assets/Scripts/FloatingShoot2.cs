using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingShoot2 : MonoBehaviour
{
    // protected
    protected Joystick joystick;

    // public
    public Rigidbody ball;
    public GameObject BasePointer;
    public Transform target;

    public float h = 25;
    public float gravity = -18;

    GameObject[] PathObjects = new GameObject[10]; 

    bool created = false;

    void Start () {
        ball.useGravity = false;
        for (int i = 0; i < PathObjects.Length; i++) {
            PathObjects[i] = Instantiate(BasePointer, transform);
            PathObjects[i].SetActive(false);
        }
    }

    void Update() {
        DrawPath();
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
		LaunchData launchData = CalculateLaunchData ();
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
