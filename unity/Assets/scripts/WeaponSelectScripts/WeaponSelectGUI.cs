using UnityEngine;
using System.Collections;

public class WeaponSelectGUI : MonoBehaviour {

	// Use this for initialization

	public GameObject WeaponDisplay;
	bool EnteringName = false;
	float cooldown=0.5f;
	float elapsedTime=0;
	Rect playerNameRect;
	string PlayerName;
	Vector3 displayPos;

	void Start () {
		playerNameRect = new Rect(10,10,100,20);
		PlayerName = "PlayerName";
	}
	void OnGUI()
	{
		displayPos = Camera.main.WorldToScreenPoint(WeaponDisplay.transform.position);
		
		//Weapon rotating 
		if (GUI.Button(new Rect(displayPos.x - 30, displayPos.y+150, 25, 25), "<<")||Input.GetKeyDown(KeyCode.LeftArrow))
		{
			if(elapsedTime>cooldown)
			{
				WeaponDisplay.GetComponent<WeaponDisplayScript>().RotateCW();
				elapsedTime =0;
			}
		}
		if (GUI.Button(new Rect(displayPos.x + 30, displayPos.y+150, 25, 25), ">>")||Input.GetKeyDown(KeyCode.RightArrow))
		{
			if(elapsedTime>cooldown)
			{
				WeaponDisplay.GetComponent<WeaponDisplayScript>().RotateCCW();
				elapsedTime =0;
			}
		}
		if(!EnteringName)
		{
			if(GUI.Button(new Rect(playerNameRect.x + 100, playerNameRect.y, 75, 25), "Edit Name"))
			{
				EnteringName = true;
			}
		}
		if(EnteringName){
			GUI.SetNextControlName("PlayerNameTF");
			PlayerName = GUI.TextField(playerNameRect, PlayerName, 25);
			GUI.FocusControl("PlayerNameTF");
			if(Event.current.keyCode == KeyCode.Return)
			{
				EnteringName = false;
			}
		}else 
		{
			GUI.Label(playerNameRect,PlayerName);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime+=Time.deltaTime;
	
	}
}
