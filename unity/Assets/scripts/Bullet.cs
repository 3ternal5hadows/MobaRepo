using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	public enum Type{fire, ice, acid, gun}
	
	public Type BulletType;
	public GameObject DeathEffect;
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision hit)
    {
        GameObject explosion;
		explosion = Instantiate(DeathEffect,this.transform.position, Quaternion.identity) as GameObject;
		
		
		Destroy(this.gameObject, 2);
		
		Destroy(GetComponent<SphereCollider>());
		transform.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		
    }
}
