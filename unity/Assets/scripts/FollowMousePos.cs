using UnityEngine;
using System.Collections;

public class FollowMousePos : MonoBehaviour {

	// Use this for initialization
	
	void Start () {
	
	}

	Vector3 currentMousePosOnScreen;
	Vector3 currentMousePosWorld;
	Vector3 centerScreen;
	Vector3 difference;
	Vector3 objectCurrentDirection;
	float roationSpeed = 1f;
			
			
	// Update is called once per frame
	void Update () {
		
		currentMousePosOnScreen = Input.mousePosition;			
		centerScreen.x = Screen.width/2.0f;
		centerScreen.y = Screen.height/2.0f;
		centerScreen.z = 0;

		currentMousePosWorld = currentMousePosOnScreen - centerScreen; 

		//difference = this.transform.forward - currentMousePosWorld.normalized; 
		



//		if(currentMousePosWorld.x>=0&&currentMousePosWorld.y>=0)
//		{
//			difference = Mathf.Sin(Mathf.Tan(currentMousePosWorld.normalized.y/currentMousePosWorld.normalized.x)) - Mathf.Sin(this.transform.eulerAngles.y); 
//		}
//		if(currentMousePosWorld.x<0&&currentMousePosWorld.y>0)
//		{
//			difference = Mathf.Sin(Mathf.PI - Mathf.Tan(currentMousePosWorld.normalized.y/currentMousePosWorld.normalized.x)) - Mathf.Sin(this.transform.eulerAngles.y); 
//		}
//		if(currentMousePosWorld.x>0&&currentMousePosWorld.y<=0)
//		{
//			difference = Mathf.Sin(2*Mathf.PI - Mathf.Tan(currentMousePosWorld.normalized.y/currentMousePosWorld.normalized.x)) - Mathf.Sin(this.transform.eulerAngles.y); 
//		}
//		if(currentMousePosWorld.x<0&&currentMousePosWorld.y<0)
//		{
//			difference = Mathf.Sin(Mathf.PI + Mathf.Tan(currentMousePosWorld.normalized.y/currentMousePosWorld.normalized.x)) - Mathf.Sin(this.transform.eulerAngles.y); 
//		}
//		Debug.Log(difference);
		
//		if(difference.y >= 0 )
//		   this.transform.Rotate(new Vector3(0,-roationSpeed,0));
//		if(difference.y < 0 )
//			this.transform.Rotate(new Vector3(0,roationSpeed,0));

		//Debug.Log(this.transform.eulerAngles);

			this.transform.LookAt(new Vector3(transform.position.x + (currentMousePosWorld.normalized.x), 
											  this.transform.position.y,
											  transform.position.z + currentMousePosWorld.normalized.y));
			
}
}
