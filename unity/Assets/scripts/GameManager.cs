using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	public enum GameMode{ Menu, Demo, NewWorkPlay};

	public GameMode currentGameState;
	void Start () {
		DontDestroyOnLoad(this.gameObject);
		currentGameState = GameMode.Menu;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
