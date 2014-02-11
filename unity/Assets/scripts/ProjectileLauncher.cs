using UnityEngine;
using System.Collections;

public class ProjectileLauncher : MonoBehaviour {

	public GameObject projectile;
	public float ReloadSpeed = 1;
	public float ChargeRate = 0.1f;

	GameObject chargingSpell;
	float scale=0.1f;
	float reloadTime;
	bool MouseJustPressed;


	void Start () {
		reloadTime=ReloadSpeed;
		MouseJustPressed= false;
	}
	
	// Update is called once per frame
	void Update () {
		if(reloadTime > ReloadSpeed)
		{

			if(Input.GetMouseButtonDown(0))
			{
				chargingSpell = Instantiate(projectile, this.transform.position, Quaternion.identity) as GameObject;
				chargingSpell.transform.parent = this.transform;
				MouseJustPressed = true;

			}
			if(Input.GetMouseButton(0))
			{
				scale+=ChargeRate*Time.deltaTime;

				//if(scale<1&&chargingSpell != null)
				//	chargingSpell.GetComponent<Projectile>().SetScale(scale);
				Debug.Log(scale);
			}

			if(Input.GetMouseButtonUp(0)&&MouseJustPressed)
			{
				MouseJustPressed = false;
 				chargingSpell.transform.parent = null;
				chargingSpell.AddComponent<Rigidbody>();
				chargingSpell.GetComponent<SphereCollider>().enabled = true;
				//if(scale<=1)chargingSpell.GetComponent<Projectile>().SetScale(scale);
				Debug.Log(scale);
				//if(scale>1)scale=1;
				chargingSpell.rigidbody.useGravity=false;
				chargingSpell.rigidbody.AddForce(transform.parent.parent.GetComponent<Rigidbody>().velocity+this.transform.forward*((3000f*scale)+300));
				scale = 0.1f;
				reloadTime = 0;
			}
		}
		reloadTime += Time.deltaTime;

	}

}
