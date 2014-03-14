using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {

	// Use this for initialization
    public float health;
	public GameObject Loot;
	float time;
	bool lootspawned;
	void Start () {
        health = 1000;
		time = Time.timeSinceLevelLoad;
		lootspawned = false;
	}
	    
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
			if(!lootspawned)
			{
				GameObject a = Instantiate(Loot, this.transform.position, Quaternion.identity)as GameObject;
				lootspawned = true;
			}
            Destroy(this.GetComponent<BoxCollider>(), 0.5f);
			Destroy(this.GetComponent<Rigidbody>(), 0.5f);
            Destroy(this.gameObject, 3);
        }
	}
    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.name == "FireBullet(Clone)")
        {			
            health -= 300;
        }
		if (hit.gameObject.name == "IceBullet(Clone)")
        {			
            health -= 300;
        }
		
		
    }
	void OnTriggerStay(Collider hit)
	{
		if(hit.gameObject.name =="LavaExplosion(Clone)"&& time < Time.timeSinceLevelLoad)
		{
			time = Time.timeSinceLevelLoad+0.5f;
			health -= 30;
		}
		if(hit.gameObject.name =="IceExplosion(Clone)"&& time < Time.timeSinceLevelLoad)
		{
			time = Time.timeSinceLevelLoad+1;
			health -= 10;
		}
	}

}
