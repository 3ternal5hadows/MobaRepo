using UnityEngine;
using System.Collections;

public class KillScript: MonoBehaviour {

	// Use this for initialization
	public int Delay=0;
	void Start () {
		Destroy(this.gameObject,Delay);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
