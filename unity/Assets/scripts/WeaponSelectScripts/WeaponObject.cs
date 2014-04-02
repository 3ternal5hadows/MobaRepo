using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponObject : MonoBehaviour {

	// Use this for initialization
	public string Name;
	public int StartingPosition;
	public int currentPosition;
	public List<GameObject> slotedWeapon;
	public bool Active = false;
	float elapsedTime=0;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime+= Time.deltaTime;
		if(Active)
		{
			foreach(GameObject weapon in slotedWeapon)
			{
				if(weapon.renderer != null)
					weapon.renderer.material.color = new Color(Mathf.Lerp(0,1.0f,elapsedTime),
				                                                            Mathf.Lerp(0,1.0f,elapsedTime),
				                                                            Mathf.Lerp(0,1.0f,elapsedTime));
				else weapon.GetComponent<SpriteRenderer>().color = new Color(Mathf.Lerp(0,1.0f,elapsedTime),
		                                                                           Mathf.Lerp(0,1.0f,elapsedTime),
		                                                                           Mathf.Lerp(0,1.0f,elapsedTime));
			}
		}else 
		{
			foreach(GameObject weapon in slotedWeapon)
			{
				if(weapon.renderer != null)
					weapon.renderer.material.color = new Color(Mathf.Lerp(1.0f,0,elapsedTime),
				                                                            Mathf.Lerp(1.0f,0,elapsedTime),
				                                                            Mathf.Lerp(1.0f,0,elapsedTime));
				else weapon.GetComponent<SpriteRenderer>().color = new Color(Mathf.Lerp(0,1.0f,elapsedTime),
				                                                                   Mathf.Lerp(0,1.0f,elapsedTime),
				                                                                   Mathf.Lerp(0,1.0f,elapsedTime));
			}
		}

	}
	public void SetActive(bool _OnOff)
	{
		if(_OnOff!=Active)
		{
			elapsedTime =0;
			Active = _OnOff;
		}
	}
}
