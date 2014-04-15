using UnityEngine;
using System.Collections;

public class MeteorSpellScript : MonoBehaviour {

	// Use this for initialization
	public GameObject deathEffect;
	public float baseDamage;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision hit)
	{
		if(hit.gameObject.tag == "bounds" || hit.gameObject.tag == "player")
		{
			Destroy(this.gameObject,1);
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			GameObject death = GameObject.Instantiate(deathEffect, this.transform.position, Quaternion.identity) as GameObject;
		}
	}
}
