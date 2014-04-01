using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponDiscScript : MonoBehaviour {

	public string SwordDisc;
	public string AxeDisc;
	public string ProjectileDisc;
	public string DaggerDisc;
	public string LineDisc;
	public string MeteorDisc;
	public string HammerDisc;
	public string ShurikenDisc;
	public string PredShurikenDisc;
	string currentDisc;
	void OnGUI()
	{
		Vector2 displayPos = Camera.main.WorldToScreenPoint(transform.position);
		GUI.Label(new Rect(displayPos.x-currentDisc.Length/2, Screen.height - 25 - displayPos.y, 200, 200),"Weapon Description");
		GUI.Label(new Rect(displayPos.x-currentDisc.Length/2, Screen.height - displayPos.y, 200, 200),currentDisc);
	}
	void Start () {
		currentDisc = SwordDisc;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
