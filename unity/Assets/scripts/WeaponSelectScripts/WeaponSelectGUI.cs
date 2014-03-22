using UnityEngine;
using System.Collections;

public class WeaponSelectGUI : MonoBehaviour {

	// Use this for initialization

	public GameObject WeaponDisplay;
	float cooldown=0.5f;
	float elapsedTime=0;
	void Start () {
	
	}
	void OnGUI()
	{
			//Weapon rotating 
		if (GUI.Button(new Rect(10, 10, 25, 25), "<<")||Input.GetKeyDown(KeyCode.LeftArrow))
		{
			if(elapsedTime>cooldown)
			{
				WeaponDisplay.GetComponent<WeaponDisplayScript>().RotateCW();
				elapsedTime =0;
			}
		}
		if (GUI.Button(new Rect(50, 10, 25, 25), ">>")||Input.GetKeyDown(KeyCode.RightArrow))
		{
			if(elapsedTime>cooldown)
			{
				WeaponDisplay.GetComponent<WeaponDisplayScript>().RotateCCW();
				elapsedTime =0;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime+=Time.deltaTime;
	
	}
}
