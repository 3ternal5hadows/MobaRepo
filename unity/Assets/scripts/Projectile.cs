using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public GameObject deathEffect;
	Vector3 localScaleStart;
	float particleStartSize;
	float timeElapsed;
	float lightRange;
	float startWidth;
	void Start () {
		localScaleStart = transform.localScale;
		particleStartSize = GetComponentInChildren<ParticleSystem>().startSize;
		lightRange = GetComponentInChildren<Light>().range;
		lightRange -= lightRange*0.5f;
		startWidth = GetComponentInChildren<TrailRenderer>().startWidth ;
	}
	public void SetScale(float scale)
	{
		//GetComponentInChildren<ParticleSystem>().startSize = particleStartSize * scale;
		transform.localScale = localScaleStart * scale;
		//GetComponentInChildren<Light>().range = lightRange * scale + (lightRange*0.5f);
		GetComponentInChildren<TrailRenderer>().startWidth = startWidth * scale;
	}
	// Update is called once per frame
	void Update () {
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
			if(gameObject.name == "ShadowBallDeath(Clone)") death.GetComponent<ExpandingExplosion>().expanding = true;
			Destroy(GetComponent<SphereCollider>());
			Network.Destroy(networkView.viewID);
		}else if(DataGod.currentGameState == DataGod.GameMode.Demo)
		{
			GameObject death = Instantiate(deathEffect, this.transform.position, Quaternion.identity) as GameObject;
			if(gameObject.name == "ShadowBallDeath(Clone)") death.GetComponent<ExpandingExplosion>().expanding = true;
			Destroy(GetComponent<SphereCollider>());
			Destroy(gameObject,1);
		}

		this.transform.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
	}	
}
