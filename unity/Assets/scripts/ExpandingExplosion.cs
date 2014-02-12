using UnityEngine;
using System.Collections;

public class ExpandingExplosion : MonoBehaviour {

	// Use this for initialization
	public float maxScale = 60;
	public float growthRate = 60;
	public bool expanding;
	bool shrinking;
	public float fallSpeed=5;
	void Start () {
		expanding = false;
		shrinking = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(expanding)
		{
			Vector3 scale = transform.localScale;
			if(transform.localScale.x<maxScale||shrinking)
				scale.x += growthRate*Time.deltaTime;

			if(transform.localScale.y<maxScale||shrinking)
				scale.y += growthRate*Time.deltaTime;

			if(transform.localScale.y<maxScale||shrinking){
				scale.z += growthRate*Time.deltaTime;

			transform.localScale = scale;
				}

			if(scale.y>maxScale&&!shrinking){
				growthRate *=-1;
				shrinking = true;
			}
			Debug.Log(scale);
			if(scale.y<0.1)Destroy(gameObject);



		}

	}
	void OnCollisionEnter(Collision hit)
	{
		expanding = true;
		this.rigidbody.useGravity = false;
		this.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
	}
}
