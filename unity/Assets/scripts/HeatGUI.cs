using UnityEngine;
using System.Collections;

public class HeatGUI : MonoBehaviour {

	// Use this for initialization
	float heat;
	void Start () {
		
		heat = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<TankMovement>().Heat;
		guiText.text = "C " + Mathf.Round(heat).ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
		heat = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<TankMovement>().Heat;
		guiText.color = new Color(heat/100, heat/100-0.4f,  1-heat/100-0.7f);
		guiText.text =  "C " + Mathf.Round(heat).ToString();
	}
}
