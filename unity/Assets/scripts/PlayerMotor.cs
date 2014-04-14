using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour
{


    //Dodge Vars
    Cooldown dodgeCooldown;
    float dodgeSpeed = 40000;

	#region PysicsVars
	Vector3 difference;   
	Vector3 accelerationGravity = Vector3.zero;
	Vector3 accelerationSpring = Vector3.zero;	
	Vector3 accelerationMovement = Vector3.zero;
    Vector3 displacement;
    Vector3 netAcceleration;
    Vector3 velocity;
    float speed = 1500f;
    float maxSpeed = 6000f;
    float frictionForce = 150;
    float gravity = 0;
	float mass = 10f;
	float gravityDeadZone = 10;
	#endregion


	float controllerDeadZone = 0.1f;


  


    void Start()
    {
        dodgeCooldown = new Cooldown(1);
        velocity = Vector3.zero;
        transform.rigidbody.freezeRotation = true;
        if (DataGod.currentGameState == DataGod.GameMode.NetWorkPlay)
        {
            if (!networkView.isMine)
            {
                Destroy(this.GetComponentInChildren<Light>());
                Destroy(this);
                Destroy(this.GetComponentInChildren<ProjectileLauncher>());
                Destroy(this.GetComponentInChildren<AttackAnimation>());
                Destroy(this.GetComponentInChildren<FollowMousePos>());

            }
            if (networkView.isMine)
            {
                DataGod.networkIsMine = true;
            }

        }
        else if (DataGod.currentGameState == DataGod.GameMode.Demo)
        {
        }
    }


    void FixedUpdate()
    {
        if (DataGod.currentGameState == DataGod.GameMode.NetWorkPlay)
        {
            if (networkView.isMine)
            {
                DetectInput();
                ResolvePhysics();
            }
        }
        else if (DataGod.currentGameState == DataGod.GameMode.Demo)
        {
            DetectInput();
            ResolvePhysics();
        }
    }
    void ResolvePhysics()
    {
        netAcceleration = accelerationSpring + accelerationGravity  + accelerationMovement;

        velocity = netAcceleration * Time.deltaTime;
        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        //velocity -= -velocity.normalized * frictionForce;		
        displacement = velocity * Time.deltaTime + 0.5f * netAcceleration * Time.deltaTime * Time.deltaTime;
        transform.rigidbody.AddForce(velocity * Time.deltaTime + 0.5f * netAcceleration * Time.deltaTime * Time.deltaTime, ForceMode.VelocityChange);

        //transform.Translate(netAcceleration);
        accelerationGravity = Vector3.zero;
        accelerationSpring = Vector3.zero;
      
      
        accelerationMovement = Vector3.zero;
    }
    void DetectInput()
    {



		#region Keyboard Input
		if (Input.GetKey(KeyCode.W))//forwards
        {
            accelerationMovement.z = 1f;
        }
        else if (Input.GetKey(KeyCode.S))//down
        {
            accelerationMovement.z = -1f;
        }
        else
        {
            accelerationMovement.z = 0;
        }
        if (Input.GetKey(KeyCode.A))//left
        {
            accelerationMovement.x = -1f;
        }
        else if (Input.GetKey(KeyCode.D))//right
        {
            accelerationMovement.x = 1f;
        }
        else
        {
            accelerationMovement.x = 0;
        }
        
		#endregion
		#region Contoller Input

		Vector2 leftStick = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
		Vector2 rightStick = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));



		if(Mathf.Abs(leftStick.x) > controllerDeadZone)
		{	
			accelerationMovement.x = leftStick.x;
		}else 
		{
			accelerationMovement.x = 0;
		}
		if(Mathf.Abs(leftStick.y) > controllerDeadZone)
		{
			accelerationMovement.z = leftStick.y;
		}else 
		{
			accelerationMovement.z = 0;
		}

		//Debug.Log(leftStick+" "+accelerationMovement);



		dodgeCooldown.Update();
		if ((Input.GetKey(KeyCode.Space)||Input.GetButton("Dodge")) && dodgeCooldown.IsOffCooldown)
		{
			dodgeCooldown.GoOnCooldown();
			accelerationMovement = accelerationMovement.normalized * (speed + dodgeSpeed);
		}
		else accelerationMovement = accelerationMovement.normalized * speed;

		



		#endregion


    }

    public void ApplyGravity(Vector3 _position, float _mass)
    {
        //radius
        difference = _position - transform.position;
        if (difference.magnitude > gravityDeadZone)
        {
            float forceOfGravity = gravity * _mass * mass / (difference.magnitude * difference.magnitude);
            accelerationGravity += difference.normalized * forceOfGravity;
        }
    }  

    
    public void ApplySpringForce(Vector3 _position, float _mass, float DeadZone, float springStrength)
    {
        difference = transform.position - _position;
        if (difference.magnitude > DeadZone)
        {
            accelerationSpring += -springStrength * (difference.magnitude - DeadZone) * difference.normalized - (springStrength * 0.5f * difference.normalized);
        }

    }
}
