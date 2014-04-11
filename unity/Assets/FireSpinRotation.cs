using UnityEngine;
using System.Collections;

public class FireSpinRotation : MonoBehaviour {

	public float lifetime;
	public float rotationSpeed;
	float elapsedTime=0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate(0,rotationSpeed,0);
		elapsedTime+= Time.deltaTime;

		if(elapsedTime>= lifetime)
		{
			Destroy(gameObject);
		}
	}
}
