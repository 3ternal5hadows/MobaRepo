using UnityEngine;
using System.Collections;

public class VideoTitle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	void OnGUI()
	{
		Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
		GUI.Label(new Rect(screenPos.x,Screen.height-screenPos.y,300,25),"Video Description");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
