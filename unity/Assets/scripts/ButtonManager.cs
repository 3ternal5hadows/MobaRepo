using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DataGod.currentGameState = DataGod.GameMode.Menu;

	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnGUI() {
        if (GUI.Button(new Rect(100, 100, 250, 100), "Server")) {
            DataGod.isClient = false;
			DataGod.currentGameState = DataGod.GameMode.NetWorkPlay;
            GoToGame();
        }
        if (GUI.Button(new Rect(100, 250, 250, 100), "Client")) {
            DataGod.isClient = true;
			DataGod.currentGameState = DataGod.GameMode.NetWorkPlay;
            GoToGame();
        }
		if (GUI.Button(new Rect(400, 100, 250, 100), "TalentTree")) {
			DataGod.isClient = false;
			DataGod.currentGameState = DataGod.GameMode.Menu;
			GoToTalents();
		}
		if (GUI.Button(new Rect(400, 250, 250, 100), "DEMO")) {
			DataGod.isClient = false;
			DataGod.currentGameState = DataGod.GameMode.Demo;
			LoadDemo();
		}

    }
	private void LoadDemo() {
		Application.LoadLevel("level 1");
	}
    private void GoToGame() {
        Application.LoadLevel("level 1");
    }
	private void GoToTalents()
	{
		Application.LoadLevel("Skill Tree");
	}
}
