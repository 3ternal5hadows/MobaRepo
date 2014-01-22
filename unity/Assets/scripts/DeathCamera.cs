using UnityEngine;
using System.Collections;

public class DeathCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.camera.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(GameObject.FindGameObjectWithTag("Player").GetComponent<TankMovement>().Health <=0 && !this.camera.enabled )
		{
			
			this.camera.enabled = true;		
			gameObject.AddComponent<AudioListener>();
		}
	
	}
}
