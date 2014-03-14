using UnityEngine;
using System.Collections;

public class AmmoRefil : MonoBehaviour {

	// Use this for initialization
	float time;
	void Start () {
		this.rigidbody.Sleep();
	}
	
	// Update is called once per frame
	void Update () {
		if(time < 3){
			time += Time.deltaTime;	
		}else this.rigidbody.WakeUp();
		
	}
	void OnCollisionEnter(Collision hit)
	{
		if(hit.gameObject.name == "Tank Player")
		{
			GameObject.FindGameObjectWithTag("TankShot").GetComponent<shoot>().resetAmmo();
			GameObject.FindGameObjectWithTag("Player").GetComponent<TankMovement>().Health+=200;	
			Destroy(this.gameObject);
		}
	}
}
