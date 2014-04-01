using UnityEngine;
using System.Collections;

public class WeaponDisplayTitle : MonoBehaviour {

	// Use this for initialization
	public string title;
	void OnGUI()
	{
		Vector2 displayPos = Camera.main.WorldToScreenPoint(transform.position);
		GUI.Label(new Rect(displayPos.x-50, Screen.height- displayPos.y, 200, 25),title);
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
