using UnityEngine;
using System.Collections;

public class CameraFollowMouse : MonoBehaviour {

	// Use this for initialization
	public GameObject Player;
	public float MaxDistanceFromPlayer = 10;
	Vector3 currentMousePosOnScreen;
	Vector3 currentMousePosWorld;
	Vector3 centerScreen;
	Vector3 CameraWorldPos;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
			currentMousePosOnScreen = Input.mousePosition;			
			centerScreen.x = Screen.width/2.0f;
			centerScreen.y = Screen.height/2.0f;
			centerScreen.z = 0;			
			currentMousePosWorld = currentMousePosOnScreen - centerScreen; 
			
			currentMousePosWorld = currentMousePosWorld/20;
			if(currentMousePosWorld.magnitude>MaxDistanceFromPlayer)
			{
				currentMousePosWorld = currentMousePosWorld.normalized*MaxDistanceFromPlayer;
			}
			CameraWorldPos = currentMousePosWorld + Player.transform.position;			
			this.transform.Translate((Player.transform.position.x+ currentMousePosWorld.x) - this.transform.position.x,			
									(Player.transform.position.y+10) - this.transform.position.y,
									(Player.transform.position.z + currentMousePosWorld.y) - this.transform.position.z, Space.World);
			
	}
}

