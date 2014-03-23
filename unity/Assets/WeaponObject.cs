using UnityEngine;
using System.Collections;

public class WeaponObject : MonoBehaviour {

	// Use this for initialization
	public int StartingPosition;
	public int currentPosition;
	public GameObject slotedWeapon;
	public bool Active = false;
	float elapsedTime=0;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime+= Time.deltaTime;
		if(Active)
		{
			slotedWeapon.gameObject.renderer.material.color = new Color(Mathf.Lerp(0,1.0f,elapsedTime),
			                                                            Mathf.Lerp(0,1.0f,elapsedTime),
			                                                            Mathf.Lerp(0,1.0f,elapsedTime));
		}else 
		{
			slotedWeapon.gameObject.renderer.material.color = new Color(Mathf.Lerp(1.0f,0,elapsedTime),
			                                                            Mathf.Lerp(1.0f,0,elapsedTime),
			                                                            Mathf.Lerp(1.0f,0,elapsedTime));
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
