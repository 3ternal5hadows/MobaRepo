using UnityEngine;
using System.Collections;

public class MouseCursor : MonoBehaviour {

	// Use this for initialization
	public float Zpos;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
		                                      Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
		                                      Zpos);
	
	}
}
