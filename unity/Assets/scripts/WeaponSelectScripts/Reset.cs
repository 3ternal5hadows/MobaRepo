using UnityEngine;
using System.Collections;

public class Reset : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI()
	{
		Vector2 displayPos = Camera.main.WorldToScreenPoint(transform.position);
		if(GUI.Button(new Rect(displayPos.x - 37.5f, Screen.height - displayPos.y + 520, 75,40), "Reset")) 
		{
			WeaponData.resetWeaponDisplay();
			foreach(WeaponDisplayScript child in this.GetComponentsInChildren<WeaponDisplayScript>())
			{
				child.SetStartingPos(0);
			}
		}
	}
}
