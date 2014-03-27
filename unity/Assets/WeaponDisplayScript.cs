using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponDisplayScript : MonoBehaviour {


	public List<GameObject>Weapons;
	private List<Vector3> WeaponStartPosition = new List<Vector3>();
	private List<WeaponObject>weaponObjects = new List<WeaponObject>();
	private GameObject currentSelectedWeapon;

	float elapsedTime=0;
	public int currentWeapon;

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

		currentWeapon = 0;
	
	}
	
	// Update is called once per frame
	void Update () {
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
		//Debug.Log("CW - currentWeapon:"+currentWeapon);

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
		//Debug.Log("CCW - currentWeapon:"+currentWeapon);
	}
}
