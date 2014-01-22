using UnityEngine;
using System.Collections;

public class BallMotor : MonoBehaviour {

	public static BallMotor instance;
	
	Vector3 difference;
	
	Vector3 accelerationGravityRight = Vector3.zero;
	Vector3 accelerationGravityLeft = Vector3.zero;
	Vector3 accelerationSpringRight = Vector3.zero;
	Vector3 accelerationSpringLeft = Vector3.zero;
	Vector3 accelerationMovement = Vector3.zero;


    public Vector3 displacement;
	Vector3 netAcceleration;
	public Vector3 velocity;
	float speed = 100000;
	float maxSpeed = 100000;
	float frictionForce = 150;
	float gravity=0;
	
	float mass=10f;
	float gravityDeadZone = 10;
	
	int powerUpCount=0;
	void Start () {
		instance = this;
		velocity = Vector3.zero;		
		transform.rigidbody.freezeRotation=true;
		
	}
	
	
	void Update () {
		
		DetectInput();
		/*if(accelerationSpringRight != Vector3.zero||accelerationGravityRight != Vector3.zero||
			accelerationSpringLeft != Vector3.zero||accelerationGravityLeft != Vector3.zero)
		{
			transform.rigidbody.useGravity = false;
		}else transform.rigidbody.useGravity = true;*/
		
		netAcceleration = accelerationSpringLeft + accelerationSpringRight + accelerationGravityLeft + accelerationGravityRight + accelerationMovement;
		;
		velocity = netAcceleration * Time.deltaTime;
		if(velocity.magnitude > maxSpeed)
		{
			velocity = velocity.normalized * maxSpeed;
		}
		
		//velocity -= -velocity.normalized * frictionForce;		
        displacement = velocity * Time.deltaTime + 0.5f * netAcceleration * Time.deltaTime * Time.deltaTime;
		transform.rigidbody.AddRelativeForce(velocity * Time.deltaTime + 0.5f * netAcceleration * Time.deltaTime * Time.deltaTime);
		accelerationGravityLeft = Vector3.zero;
		accelerationSpringLeft = Vector3.zero;
		accelerationGravityRight = Vector3.zero;
		accelerationSpringRight = Vector3.zero;
		accelerationMovement = Vector3.zero;
		
		
	}
	void DetectInput()
	{
		
		
		
		if(Input.GetKey(KeyCode.W))//forwards
		{
			
			accelerationMovement.x += Camera.main.transform.forward.normalized.x * speed;			
			accelerationMovement.z += Camera.main.transform.forward.normalized.z * speed;
			
		}		
		else if(Input.GetKey(KeyCode.S))//down
		{
			accelerationMovement.x += -Camera.main.transform.forward.normalized.x * speed;
			accelerationMovement.z += -Camera.main.transform.forward.normalized.z * speed;
		}
		else 
		{
			accelerationMovement = Vector3.zero;
			
		}
		
		if(Input.GetKey(KeyCode.A))//left
		{
			accelerationMovement.x += (Mathf.Cos (-0.5f*Mathf.PI)*Camera.main.transform.forward.x + Mathf.Sin(-0.5f*Mathf.PI)*Camera.main.transform.forward.z)*speed;
			
			accelerationMovement.z += (-Mathf.Sin (-0.5f*Mathf.PI)*	Camera.main.transform.forward.x + Mathf.Cos(-0.5f*Mathf.PI)*Camera.main.transform.forward.z) *speed;	
		}		
		else if(Input.GetKey(KeyCode.D))//right
		{
			accelerationMovement.x += (Mathf.Cos (0.5f*Mathf.PI)*	Camera.main.transform.forward.x + Mathf.Sin(0.5f*Mathf.PI)*Camera.main.transform.forward.z)*speed;
			
			accelerationMovement.z += (-Mathf.Sin (0.5f*Mathf.PI)*	Camera.main.transform.forward.x + Mathf.Cos(0.5f*Mathf.PI)*Camera.main.transform.forward.z)*speed;	
		}
		
	}
	
	public void ApplyGravityLeft(Vector3 _position, float _mass )
	{
		//radius
		difference = _position-transform.position;
		if(difference.magnitude>gravityDeadZone)
		{
			float forceOfGravity = gravity * _mass * mass / (difference.magnitude*difference.magnitude);
			accelerationGravityLeft += difference.normalized * forceOfGravity;
		}
			
	}
	public void ApplyGravityRight(Vector3 _position, float _mass )
	{
		//radius
		difference = _position-transform.position;
		if(difference.magnitude>gravityDeadZone)
		{
			float forceOfGravity = gravity * _mass * mass / (difference.magnitude*difference.magnitude);
			accelerationGravityRight += difference.normalized * forceOfGravity;
		}
			
	}
	
	public void ApplySpringForceLeft(Vector3 _position, float _mass, float DeadZone, float springStrength )
	{
		difference = transform.position - _position ;
		if(difference.magnitude>DeadZone)
		{
			accelerationSpringLeft += -springStrength * (difference.magnitude - DeadZone) * difference.normalized - (springStrength*0.5f*difference.normalized);
		}
		
	}
	public void ApplySpringForceRight(Vector3 _position, float _mass, float DeadZone, float springStrength )
	{
		difference = transform.position - _position ;
		if(difference.magnitude>DeadZone)
		{
			accelerationSpringRight += -springStrength * (difference.magnitude - DeadZone) * difference.normalized - (springStrength*0.5f*difference.normalized);
		}
		
	}
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "PowerUp")
		{
			Destroy(col.gameObject);
			powerUpCount++;
			PoweUpCount.instance.guiText.text = "poop";
		}
	}
}
