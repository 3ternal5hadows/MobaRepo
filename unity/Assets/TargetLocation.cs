using UnityEngine;
using System.Collections;

public class TargetLocation : MonoBehaviour {

	// Use this for initialization


	void Start () {	
	}

	void Update () {
		RaycastHit hit;

		Debug.DrawLine(Camera.main.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), Color.blue);

		Debug.Log("Mouse pos"+Input.mousePosition);
		Debug.Log("STW pos"+Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x ,Input.mousePosition.y,1000)));
		if(Physics.Raycast(Camera.main.transform.position,
		                   Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,1000)), out hit ))
		{
			if(hit.collider.tag == "bounds")
			{
				this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x,hit.point.x,0.2f),
				                                      Mathf.Lerp(this.transform.position.y,hit.point.y,0.2f),
				                                      Mathf.Lerp(this.transform.position.z,hit.point.z,0.2f));
			}
		}
	}
}
