using UnityEngine;
using System.Collections;

public class RotateLight : MonoBehaviour {

	// Use this for initialization
	public Vector3 Rotation;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(Rotation,Space.World);
	
	}
}
