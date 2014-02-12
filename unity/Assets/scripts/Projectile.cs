using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public GameObject deathEffect;
	Vector3 localScaleStart;
	float particleStartSize;
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
		GameObject death = Instantiate(deathEffect, this.transform.position, Quaternion.identity) as GameObject;
		if(gameObject.name == "ShadowBallDeath") death.GetComponent<ExpandingExplosion>().expanding = true;
		Destroy(gameObject);
		this.transform.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
	}	
}
