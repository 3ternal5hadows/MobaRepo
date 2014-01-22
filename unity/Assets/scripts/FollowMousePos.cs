using UnityEngine;
using System.Collections;

public class FollowMousePos : MonoBehaviour {

	// Use this for initialization
	
	void Start () {
	
	}
	public GameObject Player;
	Vector3 currentMousePosOnScreen;
	Vector3 currentMousePosWorld;
	Vector3 centerScreen;
	
	Vector3 objectCurrentDirection;
	
			
			
	// Update is called once per frame
	void Update () {
		
			currentMousePosOnScreen = Input.mousePosition;			
			centerScreen.x = Screen.width/2.0f;
			centerScreen.y = Screen.height/2.0f;
			centerScreen.z = 0;
			
			currentMousePosWorld = currentMousePosOnScreen - centerScreen; 
			
			
			
			this.transform.LookAt(new Vector3(transform.position.x + (currentMousePosWorld.normalized.x), 
											  this.transform.position.y,
											  transform.position.z + currentMousePosWorld.normalized.y));
			
	}
}
