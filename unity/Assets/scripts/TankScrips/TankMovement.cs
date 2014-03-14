using UnityEngine;
using System.Collections;

public class TankMovement : MonoBehaviour {

    public float maxSpeed = 50f;
    float frictionForce = 0.1f;
    public float acceleration = 10;
	public GameObject DeathEffect;
    public float tankSpeed=0;
    
	bool deathEffectPlaying;
    
    float jitterReduction = 0.02f;
    float roationSpeed = 40;
    Vector3 roationDirection;
	float soundLevel = 0;
	public float Heat;
	public float Health = 1000;
	public float minHeat = 44;
	
	
	float deathTimer =0;
	void Start () {
        tankSpeed = 0;        
        Heat = 30;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Heat > 120)
		{
			Health -= (Heat-100)/100;
			Heat -= 0.05f;
		}
		
		
        if (tankSpeed <= jitterReduction && tankSpeed >= -jitterReduction)
        {
            tankSpeed = 0;//Reset the tank velocity so that it doesnt shake when at low speed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (tankSpeed < maxSpeed)
            {
                tankSpeed += acceleration * Time.deltaTime;
            }
        }
        if(Input.GetKey(KeyCode.S))
        {
            if (tankSpeed > -maxSpeed)
            {
                tankSpeed += -acceleration * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            roationDirection = Vector3.zero;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            roationDirection.y = -roationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            roationDirection.y = roationSpeed * Time.deltaTime;
        }
        else roationDirection = Vector3.zero;
		
		
        
		
		if(tankSpeed > 0)
		{
			tankSpeed -= frictionForce;
		}else if (tankSpeed < 0)
		{
			tankSpeed += frictionForce;
		}else tankSpeed = 0;
		
        
		
		soundLevel = Mathf.Abs(tankSpeed)/maxSpeed+0.25f;
		
		this.audio.pitch = Mathf.Clamp(soundLevel, 0.25f, 1.2f);
        
		//stabelize heat
		if(Heat < 44)
		{
			Heat += 0.1f;
		}
		
		
		if(Health <= 0)
		{
			deathTimer += Time.deltaTime;
			if(deathTimer>5)
			{
				Application.LoadLevel("Menu");
			}
			GameObject deathEffect;
			if(!deathEffectPlaying)
			{
				deathEffect = Instantiate(DeathEffect, this.transform.position, Quaternion.identity) as GameObject;
				deathEffectPlaying = true;	
			}
			this.camera.enabled = false;
			Destroy(this.camera.GetComponent<AudioListener>());
			Destroy(this.GetComponent<TankMovement>());
			tankSpeed = 0;
		}else transform.Rotate(0, roationDirection.y, 0);
		
		transform.Translate(new Vector3(0, 0, tankSpeed * Time.deltaTime));
		
	}
	void OnCollisionEnter(Collision hit)
	{
        if (hit.gameObject.name == "EyeBallEnemy(Clone)")
        {
            Heat += 30;
            Destroy(hit.gameObject);
        }
		if(hit.gameObject.name == "TurretShot(Clone)")
		{
			Heat += 30;
			Health -= 75;
		}
		
	}
	
	void OnTriggerStay(Collider hit)
	{
		if(hit.gameObject.name == "IceExplosion(Clone)")
		{
			
			Heat -= 0.5f;
			Heat = Mathf.Clamp(Heat, -10, 10000);
		}
		if(hit.gameObject.name =="LavaExplosion(Clone)")
		{
			Heat += 0.2f;
		}
		if(hit.gameObject.name =="TurretExplosion(Clone)")
		{
			Heat += 0.2f;
		}
		if(hit.gameObject.tag =="HeatUp")
		{
			Heat += 2f;
		}
		
	}
	
}
