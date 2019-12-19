using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject Projectile;
    public GameObject Thrower;

    public Rigidbody ball;
    private Rigidbody rg;

    public Transform target;
	public float h = 25;
	public float gravity = -18;
    public float TargetSens = 4;
    public LineRenderer LineRender;
    public Material[] Elements;
    private int _currentElement = 0;
    private int _maxElement;

    
    GameObject[] PathObjects = new GameObject[10]; 
    LaunchData launchData;
    Vector3 TargetStartPos;
    Vector3 TargetBufferPos;

    // :D
    Vector3 boomPosition;
    Transform LastActionPoint;

    private void Start()
    {
        _maxElement = Elements.Length;
        Physics.gravity = Vector3.up * gravity;
        TargetStartPos = target.transform.forward + new Vector3(10, -2.8f, 0);
        TargetBufferPos = target.transform.localPosition;
       
    }

    void SetVisible() {
        LineRender.positionCount = 0;
    }

    private Material ChangeColor()
    {
       // Debug.Log(ball.GetComponent<Bomb>().Skin);
        if (_currentElement < _maxElement)
        {
           // Debug.Log(ball.GetComponent<Bomb>().Skin);
           return Elements[_currentElement++];
        }
        else
        {
            _currentElement = 0;
           return Elements[_currentElement++];

        }
       
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            LineRender.positionCount = 10;
            Vector3 inputDirection = Vector3.zero * 0.2f;
            inputDirection.x = -Input.GetAxisRaw("Mouse Y") * 0.1f;
            inputDirection.z = Input.GetAxisRaw("Mouse X") * 0.1f;
            target.transform.localPosition = TargetBufferPos + inputDirection * TargetSens;
            TargetBufferPos = target.transform.localPosition;
            DrawPath();

            if (LastActionPoint != null) {
                Destroy(LastActionPoint.gameObject);
                LastActionPoint = null;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            SetVisible();
            LastActionPoint = Instantiate(target, boomPosition, target.transform.rotation);
            LastActionPoint.gameObject.AddComponent<ActionPoint>();
            target.transform.position = TargetStartPos;
            TargetBufferPos = TargetStartPos;
           rg = Instantiate(Projectile, Thrower.transform.position, Quaternion.Euler(Camera.main.transform.rotation.eulerAngles)).GetComponent<Rigidbody>();
            rg.GetComponent<Bomb>().Skin = ChangeColor();
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
            LineRender.SetPosition(i - 1, drawPoint);
            if (i == resolution) {
                boomPosition = drawPoint;
            }
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
