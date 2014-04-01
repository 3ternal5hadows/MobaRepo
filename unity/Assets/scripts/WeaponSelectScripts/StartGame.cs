using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {
	bool loadGame = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (loadGame)
			Application.LoadLevel ("level 1");
		else {
			GUI.enabled = false;
				}
	}

	void OnGUI()
	{
		Vector2 displayPos = Camera.main.WorldToScreenPoint(transform.position);
		if(GUI.Button(new Rect(displayPos.x , Screen.height - displayPos.y - 50,110,50), "Start Game"))
		{
				DataGod.isClient = true;
				loadGame = true;
				DataGod.currentGameState = DataGod.GameMode.NetWorkPlay;
	
		}
	}
}
