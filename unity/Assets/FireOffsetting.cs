using UnityEngine;
using System.Collections;

public class FireOffsetting : MonoBehaviour {

	// Use this for initialization
	public Vector3 expansionSpeed;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(expansionSpeed,Space.Self
		                    );
	}
}

