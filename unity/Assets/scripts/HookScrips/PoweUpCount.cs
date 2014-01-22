using UnityEngine;
using System.Collections;

public class PoweUpCount : MonoBehaviour {

	// Use this for initialization
	public static PoweUpCount instance;
	void Start () {
		instance = this;
		guiText.text = string.Format("{0}",0);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}	
}
