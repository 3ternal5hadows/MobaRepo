using UnityEngine;
using System.Collections;

public class KillScript : MonoBehaviour {

	// Use this for initialization

	public float DeathCountDown=0;
	void Start () {
		Destroy(this.gameObject,DeathCountDown);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
