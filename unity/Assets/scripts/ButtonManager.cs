using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour {

	// Use this for initialization
	float elapsedTime=0;
	void Start () {
		DataGod.currentGameState = DataGod.GameMode.Menu;

	}
	public float DelayTime = 20;
	bool loadingDemo = false;
	bool loadingWeapons = false;
	bool loadingGame = false;
	
	// Update is called once per frame
	void Update () {


		if(loadingGame || loadingDemo)
		{
			elapsedTime += Time.deltaTime;
			if(elapsedTime>=DelayTime)
			{
				Application.LoadLevel("WeaponSelect");
			}
		}

	}

    void OnGUI() {
		if(!loadingDemo&&!loadingGame)
		{
	        if (GUI.Button(new Rect(100, 100, 250, 100), "Server")) {
	            DataGod.isClient = false;
				Camera.main.animation.Play();
				loadingGame = true;
				DataGod.currentGameState = DataGod.GameMode.NetWorkPlay;
	        
	        }
	        if (GUI.Button(new Rect(100, 250, 250, 100), "Client")) {
	            DataGod.isClient = true;
				Camera.main.animation.Play();
				loadingGame = true;
				DataGod.currentGameState = DataGod.GameMode.NetWorkPlay;

	        }
			if (GUI.Button(new Rect(400, 250, 250, 100), "DEMO")) {
				DataGod.isClient = false;
				Camera.main.animation.Play();
				DataGod.currentGameState = DataGod.GameMode.Demo;
				loadingDemo = true;
			}
		}else GUI.enabled = false;

    }
    

}
