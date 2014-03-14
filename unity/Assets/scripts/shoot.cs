	using UnityEngine;
using System.Collections;

public class shoot : MonoBehaviour {

	// Use this for initialization
	
	
	public Rigidbody fireShell;
	public Rigidbody iceShell;
	public Rigidbody acidShell;
	public Rigidbody gunShell;
    
	public GameObject smokeEffect;

	public int fireAmmo;
	public int iceAmmo;
	public int gunAmmo;
	public int acidAmmo;
	
	public float fireSpeed= 100;
	public float iceSpeed = 100;
	public float acidSpeed = 100;
	public float gunSpeed = 300;

	enum weapon {fire,ice,acid,gun};
	
	weapon CurrentWeaponEquiped;
	
    public float shellSpeed;
	
	public float reloadTime = 2;
	
	float currentTime; 
	float tankspeed;
	void Start () {
		currentTime = reloadTime;
		CurrentWeaponEquiped = weapon.fire;
		
	}
	
	// Update is called once per frame
	void Update () {
		//Input.GetButtonDown("Fire1")
		currentTime += Time.deltaTime;
		DetectEquipedWeapon();
		tankspeed = transform.parent.parent.parent.GetComponent<TankMovement>().tankSpeed;
		switch(CurrentWeaponEquiped)
		{
		case weapon.fire:
			if(Input.GetMouseButtonDown(0) && currentTime >= reloadTime)
			{
				if(fireAmmo>0)
				{
						
					Rigidbody bullet;
					GameObject se;
					fireAmmo--;
					Debug.Log("Fire Ammo :"+fireAmmo);
					se = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
					bullet = Instantiate(fireShell, transform.position, Quaternion.identity)as Rigidbody;
					bullet.rotation = this.transform.rotation;
					bullet.AddForce(transform.forward*200*fireSpeed+(tankspeed*transform.forward*200));
					//clone.velocity = transform.TransformDirection(Vector3.forward * 100);
					currentTime = 0;
				}
			}
			break;
		case weapon.ice:
			if(Input.GetMouseButtonDown(0) && currentTime >= reloadTime)
			{
				if(iceAmmo>0)
				{
						
					Rigidbody bullet;
					GameObject se;
					iceAmmo--;
					Debug.Log("ice Ammo :"+iceAmmo);
					se = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
					bullet = Instantiate(iceShell, transform.position, Quaternion.identity)as Rigidbody;
					bullet.rotation = this.transform.rotation;
					bullet.AddForce(transform.forward*200*iceSpeed+(tankspeed*transform.forward*200));
					//clone.velocity = transform.TransformDirection(Vector3.forward * 100);
					currentTime = 0;
				}
			}
			break;
		case weapon.gun:
			if(Input.GetMouseButtonDown(0) && currentTime >= reloadTime)
			{
				if(gunAmmo>0)
				{
						
					Rigidbody bullet;
					GameObject se;
					gunAmmo--;
					Debug.Log("gun Ammo :"+gunAmmo);
					se = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
					bullet = Instantiate(gunShell, transform.position, Quaternion.identity)as Rigidbody;
					bullet.rotation = this.transform.rotation;
					bullet.AddForce(transform.forward*200*gunSpeed+(transform.forward*tankspeed));
					//clone.velocity = transform.TransformDirection(Vector3.forward * 100);
					currentTime = reloadTime - 0.5f;
				}
			}
			break;
		case weapon.acid:
			if(Input.GetMouseButtonDown(0) && currentTime >= reloadTime)
			{
				if(acidAmmo>0)
				{
						
					Rigidbody bullet;
					GameObject se;
					acidAmmo--;
					Debug.Log("acid Ammo :"+acidAmmo);
					se = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
					bullet = Instantiate(acidShell, transform.position, Quaternion.identity)as Rigidbody;
					bullet.rotation = this.transform.rotation;
					bullet.AddForce(transform.forward*200*acidSpeed+(transform.forward*tankspeed));
					//clone.velocity = transform.TransformDirection(Vector3.forward * 100);
					currentTime = 0;
				}
			}
			break;			
		}
	}
	void DetectEquipedWeapon()
	{
		if(Input.GetKey(KeyCode.Alpha1))
		{
			Debug.Log("1");
			CurrentWeaponEquiped = weapon.fire;
		}
		else if(Input.GetKey(KeyCode.Alpha2))
		{
			Debug.Log("2");
			CurrentWeaponEquiped = weapon.ice;
		}
		else if(Input.GetKey(KeyCode.Alpha3))
		{
			Debug.Log("3");
			CurrentWeaponEquiped = weapon.acid;
		}
		else if(Input.GetKey(KeyCode.Alpha4))
		{
			Debug.Log("4");
			CurrentWeaponEquiped = weapon.gun;
		}
	}
	
	
	
	class Weapon{
		float shellSpeed;
		float weaponHitDamage;
		float weaponDot;		
		public Weapon(){}
	}
	public void resetAmmo()
	{
		fireAmmo+=10;
		iceAmmo+=10;
	}
    
	
	
}
