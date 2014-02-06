using UnityEngine;
using System.Collections;

public class CameraFollowMouse : MonoBehaviour {

	// Use this for initialization
	public GameObject Player;
	public float MaxDistanceFromPlayer = 13f;
	Vector3 currentMousePosOnScreen;
	Vector3 currentMousePosWorld;
	Vector3 centerScreen;
	Vector3 CameraWorldPos;
	public float DeadZone = 10f;
	void Awake()
	{
		Application.targetFrameRate = 60;
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		currentMousePosOnScreen = Input.mousePosition;	
		//Debug.Log (currentMousePosOnScreen);
		centerScreen.x = Screen.width/2.0f;
		centerScreen.y = Screen.height/2.0f;
		centerScreen.z = 0;			
		currentMousePosWorld = currentMousePosOnScreen - centerScreen; 
		
		currentMousePosWorld = currentMousePosWorld/40f;
		if(currentMousePosWorld.magnitude<DeadZone)
		{
			currentMousePosWorld = Vector3.zero;
		}
		else{
			currentMousePosWorld = currentMousePosWorld.normalized * (currentMousePosWorld.magnitude - DeadZone);
		}

		if(currentMousePosWorld.magnitude>MaxDistanceFromPlayer)
		{
			currentMousePosWorld = currentMousePosWorld.normalized*MaxDistanceFromPlayer;
		}
		//CameraWorldPos = currentMousePosWorld + Player.transform.position;
		/*if((this.transform.position-(Player.transform.position+currentMousePosWorld)).magnitude > 0.01f)
		{
		this.transform.Translate(Mathf.Lerp(this.transform.position.x, Player.transform.position.x + currentMousePosWorld.x ,0.1f),
		                         Mathf.Lerp(this.transform.position.y, Player.transform.position.y+10f,0.1f),
		                         Mathf.Lerp(this.transform.position.z, Player.transform.position.z + currentMousePosWorld.y, 0.1f),Space.World);
		}*/
		this.transform.Translate((Player.transform.position.x+ currentMousePosWorld.x) - this.transform.position.x,			
								(Player.transform.position.y+20) - this.transform.position.y,
								(Player.transform.position.z + currentMousePosWorld.y) - this.transform.position.z, Space.World);
		
	}
}

