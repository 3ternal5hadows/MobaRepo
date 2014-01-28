using UnityEngine;
using System.Collections;

public class setFloorHeight : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.transform.Translate(0,-0.5f-this.transform.position.y,0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
