using UnityEngine;
using System.Collections;

public class Expanding : MonoBehaviour {

	// Use this for initialization
	public float maxScale = 15;
	public float growthRate = 1;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 scale = transform.localScale;
		if(transform.localScale.x<maxScale)
			scale.x += growthRate*Time.deltaTime;

		if(transform.localScale.y<maxScale)
			scale.y += growthRate*Time.deltaTime;

		if(transform.localScale.y<maxScale)
			scale.z += growthRate*Time.deltaTime;
		transform.localScale = scale;
	}
}
