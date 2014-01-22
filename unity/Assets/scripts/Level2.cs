using UnityEngine;
using System.Collections;

public class Level2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseEnter()
	{
		renderer.material.color = Color.red;
	}
	void OnMouseExit()
	{
		renderer.material.color = Color.white;
	}
	void OnMouseUp()
	{
		Application.LoadLevel("VolcanoTank");
		Application.Quit();
	}
}
