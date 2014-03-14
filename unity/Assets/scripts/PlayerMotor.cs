using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour {
	
	Vector3 difference;	
	Vector3 accelerationGravityRight = Vector3.zero;
	Vector3 accelerationGravityLeft = Vector3.zero;
	Vector3 accelerationSpringRight = Vector3.zero;
	Vector3 accelerationSpringLeft = Vector3.zero;
	Vector3 accelerationMovement = Vector3.zero;

	//Dodge Vars
	float timeSinceLastDodge = 0;
	float dodgeSpeed = 40000;
	float dodgeCooldown = 1f;

     Vector3 displacement;
	 Vector3 netAcceleration;
	 Vector3 velocity;
	float speed = 1500f;
	float maxSpeed = 6000f;
	float frictionForce = 150;
	float gravity=0;
	
	float mass=10f;
	float gravityDeadZone = 10;
	

	void Start () {
		velocity = Vector3.zero;		
		transform.rigidbody.freezeRotation=true;
		if(DataGod.currentGameState == DataGod.GameMode.NetWorkPlay)
		{
			if(!networkView.isMine)
			{
				Destroy(this.GetComponentInChildren<Light>());
				Destroy(this);
				Destroy(this.GetComponentInChildren<ProjectileLauncher>());
				Destroy(this.GetComponentInChildren<AttackAnimation>());
				Destroy(this.GetComponentInChildren<FollowMousePos>());

			}
			if(networkView.isMine)
			{
				DataGod.networkIsMine = true;
			}

		}else if(DataGod.currentGameState == DataGod.GameMode.Demo)
		{
		}
	}
	
	
	void FixedUpdate () {
		if(DataGod.currentGameState == DataGod.GameMode.NetWorkPlay)
		{
			if(networkView.isMine)
			{
				DetectInput();	

				ResolvePhysics();
			}
		}else if(DataGod.currentGameState == DataGod.GameMode.Demo)
		{
			DetectInput();
			ResolvePhysics();
		}
	}
	void ResolvePhysics()
	{
		netAcceleration = accelerationSpringLeft + accelerationSpringRight + accelerationGravityLeft + accelerationGravityRight + accelerationMovement;
		
		velocity = netAcceleration * Time.deltaTime;
		if(velocity.magnitude > maxSpeed)
		{
			velocity = velocity.normalized * maxSpeed;
		}
		
		//velocity -= -velocity.normalized * frictionForce;		
		displacement = velocity * Time.deltaTime + 0.5f * netAcceleration * Time.deltaTime * Time.deltaTime;
		transform.rigidbody.AddForce(velocity * Time.deltaTime + 0.5f * netAcceleration * Time.deltaTime * Time.deltaTime,ForceMode.VelocityChange);
		
		//transform.Translate(netAcceleration);
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
			accelerationMovement.z =1f;
		}		
		else if(Input.GetKey(KeyCode.S))//down
		{
			accelerationMovement.z += -1f;
		}
		else 
		{			
			accelerationMovement.z = 0;		
		}
		
		if(Input.GetKey(KeyCode.A))//left
		{
			accelerationMovement.x =-1f;
		}		
		else if(Input.GetKey(KeyCode.D))//right
		{
			accelerationMovement.x = 1f;
		}
		else 
		{			
			accelerationMovement.x = 0;		
		}
		timeSinceLastDodge += Time.deltaTime;
		if(Input.GetKey(KeyCode.Space) && timeSinceLastDodge > dodgeCooldown)
		{
			timeSinceLastDodge = 0;
			accelerationMovement = accelerationMovement.normalized * (speed + dodgeSpeed);
		}else accelerationMovement = accelerationMovement.normalized * speed;
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

	}
}
