using UnityEngine;
using System.Collections;

public class ShurikenScript : MonoBehaviour {
	public float pullBackTmr;
	private float elapsedTime;
	public GameObject player;
	public Vector3 velocity;
	public Vector3 acceleration;
	public float rotationalVelocity;
	public float rotationalAcceleration;

	// Use this for initialization
	void Awake(){
				velocity = Vector3.zero;
				acceleration = Vector3.zero;
				rotationalVelocity = 0.0f;
				rotationalAcceleration = 0.0f;
		}
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		velocity += acceleration * Time.deltaTime;
		transform.Translate(velocity * Time.deltaTime + (0.5f * acceleration * Time.deltaTime * Time.deltaTime), Space.World);
		acceleration = Vector3.zero; 
		rotationalVelocity += rotationalAcceleration * Time.deltaTime;
		this.transform.Rotate(new Vector3(0,rotationalVelocity,0));
		rotationalAcceleration = 0;

		if (elapsedTime >= pullBackTmr)
						this.addSpringForce (100.0f, 0.0f);
	}

	void OnTriggerEnter(Collider collider) {
		Debug.Log ("hit");
		if (collider.gameObject == player && !(elapsedTime <= pullBackTmr))
						Destroy (this.gameObject);
		}

	public void addSpringForce(float _springStrength, float _springAtRest)
	{
		Vector3 difference =  this.transform.position- player.transform.position;
		float distance = difference.magnitude;
		acceleration += -_springStrength * difference.normalized;
	}
	public void addForce(float _force,Vector3 _direction)
	{
		acceleration += _direction.normalized * _force;
	}
	public void addRotationalForce(float _force, float _radius)
	{
		rotationalAcceleration += _radius * _force;
	}
}
