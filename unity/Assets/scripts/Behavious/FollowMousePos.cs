using UnityEngine;
using System.Collections;

public class FollowMousePos : MonoBehaviour {

	// Use this for initialization
	
	void Start () {
	
	}

	Vector3 currentMousePosOnScreen;
	Vector3 currentMousePosWorld;
	Vector3 centerScreen;
	float difference;
	Vector3 objectCurrentDirection;
	float rotationSpeed = 200f;
	float mouseAngleInRadians;	
	float thisAngle;
	float differenceAngle;
			
	// Update is called once per frame
	float UnitToRadian(Vector3 unit)
	{
		return Mathf.Atan2(unit.y,unit.x);
//		if(unit.normalized.x >= 0 && unit.normalized.y >= 0) 
//			return Mathf.Atan2(unit.normalized.y,unit.normalized.x);
//		if(unit.normalized.x < 0 && unit.normalized.y >= 0) 
//			return Mathf.PI - Mathf.Atan2(unit.normalized.y,unit.normalized.x);
//		if(unit.normalized.x < 0 && unit.normalized.y < 0) 
//			return Mathf.PI + Mathf.Atan2(unit.normalized.y,unit.normalized.x);
//		if(unit.normalized.x >= 0 && unit.normalized.y >= 0) 
//			return Mathf.PI*2 - Mathf.Atan2(unit.normalized.y,unit.normalized.x);
//		return 0f;
	}
	float UnitToRadian(Vector2 unit)
	{
		return Mathf.Atan2(unit.y,unit.x);
//		if(unit.x >= 0 && unit.y >= 0) 
//			return Mathf.Atan2(unit.y,unit.x);
//		if(unit.x < 0 && unit.y >= 0) 
//			return Mathf.PI - Mathf.Atan2(unit.y,unit.x);
//		if(unit.x < 0 && unit.y < 0) 
//			return Mathf.PI + Mathf.Atan2(unit.y,unit.x);
//		if(unit.x >= 0 && unit.y >= 0) 
//			return Mathf.PI*2 - Mathf.Atan2(unit.y,unit.x);
//		return 0f;
	}

	void Update () {
		
		currentMousePosOnScreen = Input.mousePosition;			
		centerScreen.x = Screen.width/2.0f;
		centerScreen.y = Screen.height/2.0f;
		centerScreen.z = 0;

		currentMousePosWorld = currentMousePosOnScreen - centerScreen; 
	
		mouseAngleInRadians = UnitToRadian(currentMousePosWorld);
		mouseAngleInRadians = (mouseAngleInRadians < 0) ? mouseAngleInRadians + 2 * Mathf.PI : mouseAngleInRadians;

		thisAngle = UnitToRadian(new Vector2(this.transform.forward.x,this.transform.forward.z));
		thisAngle = (thisAngle < 0) ? thisAngle + 2 * Mathf.PI : thisAngle;

		difference = mouseAngleInRadians - thisAngle;

		//Debug.Log(UnitToRadian(currentMousePosWorld));

		if(difference > Mathf.PI || difference < -Mathf.PI)
		{
			difference = -difference;
		}
		if(Mathf.Abs(difference)>rotationSpeed/(180/Mathf.PI)*Time.deltaTime)
		{
			Debug.Log("N "+difference/(Mathf.PI/180));
			if(difference > 0)
			{

				this.transform.Rotate(0,-rotationSpeed*Time.deltaTime,0);
			}else 
			{
				this.transform.Rotate(0,rotationSpeed*Time.deltaTime,0);
			}
		}else
		{
			Debug.Log("difference"+difference/(Mathf.PI/180));
			this.transform.Rotate(new Vector3(0,1,0),-difference/(Mathf.PI/180));
		}


					


		//Debug.Log(this.transform.forward);
		//Debug.Log(differenceAngle);


	}
}
