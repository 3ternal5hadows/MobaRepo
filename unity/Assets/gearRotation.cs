using UnityEngine;
using System.Collections;

public class gearRotation : MonoBehaviour {

	// Use this for initialization

	public float Rotation;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,0,Rotation);
	}
}
