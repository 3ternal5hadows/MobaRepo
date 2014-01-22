using UnityEngine;
using System.Collections;

public class AIBarrel : MonoBehaviour {

	// Use this for initialization
	public GameObject projectile;
	public float speed = 50000;
	float reloadTime = 4;
	float currentTime =0; 
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if(this.transform.parent.parent.GetComponent<TurretAiRotation>().playerWithinRange)
		{
			currentTime+=Time.deltaTime;
			if(currentTime>=reloadTime)
			{	
				currentTime =0;
				
				GameObject p = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
				p.rigidbody.AddForce(transform.forward * speed);
			}
		}
	
	}
}
