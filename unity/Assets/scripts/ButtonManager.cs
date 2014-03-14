using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnGUI() {
        if (GUI.Button(new Rect(100, 100, 250, 100), "Server")) {
            DataGod.isClient = false;
            GoToGame();
        }
        if (GUI.Button(new Rect(100, 250, 250, 100), "Client")) {
            DataGod.isClient = true;
            GoToGame();
        }
		if (GUI.Button(new Rect(400, 100, 250, 100), "TalentTree")) {
			DataGod.isClient = false;
			GoToTalents();
		}
    }

    private void GoToGame() {
        Application.LoadLevel("level 1");
    }
	private void GoToTalents()
	{
		Application.LoadLevel("Skill Tree");
	}
}
