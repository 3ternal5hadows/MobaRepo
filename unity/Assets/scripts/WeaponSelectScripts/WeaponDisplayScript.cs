using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponDisplayScript : MonoBehaviour {


	public List<GameObject>Weapons;
	private List<Vector3> WeaponStartPosition = new List<Vector3>();
	private List<WeaponObject>weaponObjects = new List<WeaponObject>();
	private GameObject currentSelectedWeapon;
	float cooldown=0.5f;
	float elapsedTime=0;
	float switchTime=0;
	public int DisplayNumber;


	public int currentWeapon;
	void OnGUI()
	{
		//WeaponRotating
		Vector2 displayPos = Camera.main.WorldToScreenPoint(transform.position);

		if (GUI.Button(new Rect(displayPos.x - 40, Screen.height+30 - displayPos.y, 25, 25), "<<"))
		{
			if(elapsedTime>cooldown)
			{
				RotateCW();
				switchTime =0;
			}
		}
		if (GUI.Button(new Rect(displayPos.x + 18,Screen.height+30 - displayPos.y, 25, 25), ">>"))
		{
			if(elapsedTime>cooldown)
			{
				RotateCCW();
				switchTime =0;
			}
		}

		if(GUI.Button (new Rect(displayPos.x+100, Screen.height+30 - displayPos.y, 125, 25),"Setup Talents"))
		{
			WeaponData.currentTree = DisplayNumber;
			Application.LoadLevel("Skill Tree");


		}

		GUI.Label(new Rect(displayPos.x + 100, Screen.height- displayPos.y, 100,25), currentSelectedWeapon.GetComponent<WeaponObject>().Name);
	}

	// Use this for initialization
	void Start () {
		int count;
		foreach(GameObject weapon in Weapons)
		{
			WeaponStartPosition.Add(weapon.transform.position);
			weaponObjects.Add(weapon.GetComponent<WeaponObject>());
		}

		if(Weapons != null)
			currentSelectedWeapon = Weapons[0];
		currentWeapon=0;
		SetStartingPos(WeaponData.weapons[DisplayNumber]);


	
	}
	
	// Update is called once per frame
	void Update () {
		currentSelectedWeapon = Weapons[currentWeapon];
		int count=0;
		elapsedTime += Time.deltaTime;
		foreach(GameObject weapon in Weapons)
		{			
			weapon.transform.position = new Vector3(Mathf.Lerp(weapon.transform.position.x,WeaponStartPosition[weaponObjects[count].currentPosition].x,elapsedTime),
			                                        Mathf.Lerp(weapon.transform.position.y,WeaponStartPosition[weaponObjects[count].currentPosition].y,elapsedTime),
			                                        Mathf.Lerp(weapon.transform.position.z,WeaponStartPosition[weaponObjects[count].currentPosition].z,elapsedTime));
			if(weaponObjects[count].StartingPosition == currentWeapon){

				weaponObjects[count].SetActive(true);
			}
      		else{
				weaponObjects[count].SetActive(false);
			}
			count++;
		}

	
	}
	public void RotateCW(){
		currentWeapon--;
		
		elapsedTime =0;
		if(currentWeapon<0)
		{
			currentWeapon = Weapons.Count - 1;
		}
	
		foreach(WeaponObject weapon in weaponObjects)
		{
			weapon.currentPosition++;
			if(weapon.currentPosition>weaponObjects.Count-1)
			{
				weapon.currentPosition=0;
			}

		}
		WeaponData.weapons[DisplayNumber] = currentWeapon;
		//Debug.Log("CW - currentWeapon:"+currentWeapon);

	}
	public void SetStartingPos(int startWeapon)
	{
		while(currentWeapon != startWeapon)
		{
			currentWeapon++;		

			if(currentWeapon>Weapons.Count - 1)
			{
				currentWeapon = 0;
			}
			
			foreach(WeaponObject weapon in weaponObjects)
			{
				weapon.currentPosition--;
				if(weapon.currentPosition<0)
				{
					weapon.currentPosition=weaponObjects.Count-1;
				}			
			}
		}
	}
	public void RotateCCW()
	{
		currentWeapon++;		
		elapsedTime = 0;
		if(currentWeapon>Weapons.Count - 1)
		{
			currentWeapon = 0;
		}

		foreach(WeaponObject weapon in weaponObjects)
		{
			weapon.currentPosition--;
			if(weapon.currentPosition<0)
			{
				weapon.currentPosition=weaponObjects.Count-1;
			}

		}
		WeaponData.weapons[DisplayNumber] = currentWeapon;
		//Debug.Log("CCW - currentWeapon:"+currentWeapon);
	}
}
