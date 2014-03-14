using UnityEngine;
using System.Collections;

public class TurretTurning : MonoBehaviour {

    public float sensitivity = 5;
	void Update ()
	{
		if(GameObject.FindGameObjectWithTag("Player").GetComponent<TankMovement>().Health>0)
		{			
			transform.Rotate(0,Input.GetAxis("Mouse X") * sensitivity ,0);
		}
	}
	
	void Start ()
	{
		// Make the rigid body not change rotation
		if (rigidbody)
			rigidbody.freezeRotation = true;
	}
}
