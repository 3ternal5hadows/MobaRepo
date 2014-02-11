using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {

	// Use this for initialization
	public GameObject DeathEffect;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision hit)
	{
		if(hit.gameObject.tag == "Player")
		{
			GameObject death;
			death = Instantiate(DeathEffect, this.transform.position, Quaternion.identity)as GameObject;
			Destroy(gameObject);
		}
	}
}
