using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour {	
	
	public Vector3 collisionLocation;
	public static float mass = 100;
	public Vector3 velocity;
	float speed = 3f; 
	public bool collided;
	
	
	void Start () {
		collided = false;		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!collided)
		{			
			this.transform.Translate(velocity*speed, Space.World);
			
			
		}
	}
	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Hookable" && !collided)
		{
			Debug.Log(col.gameObject.name+" hooked");
			collisionLocation = this.transform.position;
			
			velocity = Vector3.zero;
			this.rigidbody.velocity = Vector3.zero;
			this.rigidbody.freezeRotation = true;
			this.rigidbody.drag = 1000;
			collided = true;
		}
		
	}
	
}
