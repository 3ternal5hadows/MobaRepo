using UnityEngine;
using System.Collections;

public class Barrle : MonoBehaviour {

	public float sensitivity = 5F;
	public float min;
	public float max;
	float rotation=0;

	void Update ()
	{
		rotation -= Input.GetAxis("Mouse Y") * sensitivity;
		rotation = Mathf.Clamp (rotation, min, max);
        transform.localEulerAngles = new Vector3(0, 90, rotation);
		
	}
	
	void Start ()
	{
		
		if (rigidbody)
			rigidbody.freezeRotation = true;
	}
}
