using UnityEngine;
using System.Collections;

public class NormalShuriken : MonoBehaviour {

	public GameObject deathEffect;
	float timeElapsed;

	void Start ()
	{

	}

	
	// Update is called once per frame
	void Update ()
	{
		if(rigidbody!=null)
		{
			this.transform.LookAt(rigidbody.velocity+this.transform.position);
		}	
	}

	void OnCollisionEnter(Collision hit)
	{
		if(DataGod.currentGameState == DataGod.GameMode.NetWorkPlay)
		{
			GameObject death = Network.Instantiate(deathEffect, this.transform.position, Quaternion.identity,0) as GameObject;
			Destroy(GetComponent<SphereCollider>());
			Network.Destroy(networkView.viewID);
		}else if(DataGod.currentGameState == DataGod.GameMode.Demo)
		{
			GameObject death = Instantiate(deathEffect, this.transform.position, Quaternion.identity) as GameObject;
			Destroy(GetComponent<SphereCollider>());
			Destroy(gameObject,1);
		}
		
		this.transform.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
	}	
}
