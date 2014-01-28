//	<- 2 Hook Grapple System ->
/*	
 * 	coder: Wesley Allard 
 * 	
 * 	This Grappling hook is a # step system
 * 	Step 1: -Detect nearby object with raycast
 * 			-If raycast misses then rotate and repeat	 
 * 	Step 2: -Shoot hook in the direction of raycast		
 * 
 */
using UnityEngine;
using System.Collections;

public class HookShot : MonoBehaviour {	
	
	public static HookShot instance;
	public GameObject Hook;
	float HookSpeed = 3;
	
	GameObject rightHook;
	GameObject leftHook;	


	Vector3 leftDifference;
	Vector3 rightDifference;
	
	Vector3 Direction;
	
	Vector3 leftHookLocation, rightHookLocation;
		
	Vector3 leftHookDirection, rightHookDirection;

	bool LeftHookMoving, RightHookMoving;
	bool objectFoundLeft, objectFoundRight;
	bool LeftHookAttached, RightHookAttached;
	
	
	
	public int MaxHooks = 8;
	int currentHookLeft=0;
	int currentHookRight=0;
	public float HookRotation = 8f;
	public float HookLength = 1000;	
	
	public float springStrength=2000;
	public float springAtRest = 0;
	
	RaycastHit hitLeft;
	RaycastHit hitRight;
	
	
	
	void Start () {
		instance = this;		
	}
	
	
	void Update () {
		//Left Hook Detection;
		if (Input.GetMouseButton(0))// && !LeftHookMoving)
		{
						
			transform.rotation = Camera.main.transform.rotation;			
			currentHookLeft = 0;
			
			while(!objectFoundLeft && currentHookLeft != MaxHooks){
				
				Direction = new Vector3(this.transform.forward.x,this.transform.forward.y,this.transform.forward.z);
				Direction.Normalize();
				if(Physics.Raycast(this.transform.position,Direction,out hitLeft, HookLength)&&!objectFoundLeft)
				{
					if(hitLeft.collider.gameObject.tag == "Hookable")
					{
						objectFoundLeft = true;
						Debug.DrawLine(this.transform.position, this.transform.position + (Direction * HookLength),Color.blue);
						leftHookDirection =  new Vector3(Direction.x,Direction.y, Direction.z);//Save the direction of the line so we may use it later
					}
					else
					{	//Simple fix, but needs to be updated to be modular
						Debug.DrawLine(this.transform.position, this.transform.position + (Direction * HookLength),Color.red);
						this.transform.Rotate(0, -HookRotation, 0);
						currentHookLeft++;
					}
				}else if(currentHookLeft<MaxHooks)
				{
					Debug.DrawLine(this.transform.position, this.transform.position + (Direction * HookLength),Color.red);
					this.transform.Rotate(0, -HookRotation, 0);
					currentHookLeft++;
				}
				
			}
			if(objectFoundLeft && !LeftHookMoving && !LeftHookAttached)
			{
				LeftHookMoving = true;
				shootLeftHook(currentHookLeft);
			}
			else if(LeftHookMoving)
			{
				if(leftHook.GetComponent<Hook>().collided)
				{
					leftHookLocation = leftHook.GetComponent<Hook>().collisionLocation;
					Debug.Log("Left hook hit something @ "+leftHookLocation.ToString());
					LeftHookMoving = false;
					LeftHookAttached = true;					
				}				
			}
			if(LeftHookAttached)
			{
				leftDifference = leftHookLocation - this.transform.position;
				//Formula for gravity;
				GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>().ApplyGravityLeft(leftHookLocation,100f);
				GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>().ApplySpringForceLeft(leftHookLocation, 100f, springAtRest, springStrength);
				//gravityForceLeft = leftDifference.normalized*gravity*mass*leftHook.GetComponent<Hook>().mass/(leftDifference.magnitude*leftDifference.magnitude);
				//springForceLeft = -springStrength*(leftDifference.magnitude - springAtRest)*(leftDifference.normalized);				
				
			}
						
		}else
		{
			objectFoundLeft = false;	//Reset variables
			LeftHookMoving = false;
			LeftHookAttached = false;
			
			Destroy(leftHook);
		}
		
		//Right Hook Detection;
		if (Input.GetMouseButton(1))// && !RightHookMoving)
		{
			
			this.transform.rotation = Camera.main.transform.rotation;			
			currentHookRight = 0;
			
			while(!objectFoundRight && currentHookRight != MaxHooks && !RightHookAttached){				
				
				Direction = new Vector3(this.transform.forward.x,this.transform.forward.y,this.transform.forward.z);
				Direction.Normalize();
				
				if(Physics.Raycast(this.transform.position,Direction,out hitRight, HookLength)&&!objectFoundRight)
				{
					if(hitRight.collider.gameObject.tag == "Hookable")
					{						
						objectFoundRight = true;
						Debug.DrawLine(this.transform.position, this.transform.position + (Direction * HookLength),Color.blue);//Draw the line that collided with something
						rightHookDirection = new Vector3(Direction.x, Direction.y, Direction.z);	//Save the direction of the line so we may use it later
					}
					else 
					{
						currentHookRight++;
						Debug.DrawLine(this.transform.position, this.transform.position + (Direction * HookLength),Color.red);
						this.transform.Rotate(0, HookRotation, 0);
					}
				}else if(currentHookRight<MaxHooks)
				{
					currentHookRight++;
					Debug.DrawLine(this.transform.position, this.transform.position + (Direction * HookLength),Color.red);
					this.transform.Rotate(0, HookRotation, 0);
				}
				
			}
			if(objectFoundRight && !RightHookMoving && !RightHookAttached)
			{
				RightHookMoving = true;
				shootRightHook(currentHookRight);
			}
			else if(RightHookMoving)
			{
				if(rightHook.GetComponent<Hook>().collided)
				{
					rightHookLocation = rightHook.GetComponent<Hook>().collisionLocation;
					Debug.Log("Right hook hit something @ "+rightHookLocation.ToString());
					RightHookMoving = false;
					RightHookAttached = true;
				}				
			}	
			if(RightHookAttached)
			{
				rightDifference =  rightHookLocation - this.transform.position;
				//Formula for gravity;
				GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>().ApplyGravityRight(rightHookLocation,100f);
				GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>().ApplySpringForceRight(rightHookLocation, 100f, springAtRest, springStrength);
				//gravityForceRight = rightDifference.normalized*gravity*mass*rightHook.GetComponent<Hook>().mass/(rightDifference.magnitude*rightDifference.magnitude);
				//springForceRight = -springStrength*(rightDifference.magnitude - springAtRest)*(rightDifference.normalized);								
			}
		}else
		{
			objectFoundRight = false;	//Reset variables
			RightHookMoving = false;
			RightHookAttached = false;
	
			Destroy(rightHook);
		}
	
		
		
			
	}
	void shootRightHook(int _currentHook)
	{
		rightHook = Instantiate(Hook, transform.position, Quaternion.identity) as GameObject;		
		rightHook.GetComponent<Hook>().velocity = rightHookDirection.normalized * HookSpeed;		
		rightHook.transform.Rotate(0,_currentHook * HookRotation,0);	
		
	}
	void shootLeftHook(int _currentHook)
	{
		leftHook = Instantiate(Hook, transform.position, Quaternion.identity) as GameObject;
		leftHook.GetComponent<Hook>().velocity = leftHookDirection.normalized * HookSpeed;		
		leftHook.transform.Rotate(0,_currentHook * HookRotation,0);	
		
	}
}
	































