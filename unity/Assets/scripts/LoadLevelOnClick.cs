using UnityEngine;
using System.Collections;

public class LoadLevelOnClick : MonoBehaviour {


	public string LevelName;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void LoadLevel()
	{
		Application.LoadLevel(LevelName);
	}
}
