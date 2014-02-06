using UnityEngine;
using System.Collections;

public class CountDown : MonoBehaviour {

	// Use this for initialization
	float time;
	void Start () {
		time = 300;
	}
	
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		guiText.text = "Time Left:"+Mathf.Floor(time/60f)+":"+Mathf.Floor(time%60);
	}
	public void resetTimer()
	{
		time = 300;
	}
}
