using UnityEngine;
using System.Collections;

public class iceammogui : MonoBehaviour {

	// Use this for initialization
	void Start () {
		guiText.text = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<shoot>().iceAmmo.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		guiText.text = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<shoot>().iceAmmo.ToString();
	}
}
