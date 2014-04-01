using UnityEngine;
using System.Collections;

public class WeaponSelectGUI : MonoBehaviour {

	// Use this for initialization


	bool EnteringName = false;

	Rect playerNameRect;
	string PlayerName;
	Vector3 displayPos;

	void Start () {
		playerNameRect = new Rect(10,10,100,20);
		PlayerName = "PlayerName";
	}
	void OnGUI()
	{
	

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

	
	}
}
