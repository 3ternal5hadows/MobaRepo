using UnityEngine;
using System.Collections;

public class DissolvingDoor : MonoBehaviour {

	public float sliceAmount;//0-1, 1 is full slice 
					  //	 0 is none
	public float sliceSpeed =1;
	bool playerOn = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		renderer.material.SetFloat("_SliceAmount",sliceAmount);
		if(playerOn)
		{
			playerOn=false;
		}else 
		{
			sliceAmount-=sliceSpeed*Time.deltaTime;
		}
		if(sliceAmount<0)sliceAmount=0;
	}
	void OnTriggerStay(Collider hit)
	{
		if(hit.gameObject.tag == "Player")
		{
			playerOn = true;
			//Debug.Log("Player On Door "+sliceAmount);
			sliceAmount += sliceSpeed *Time.deltaTime;
			if(sliceAmount>1){
				sliceAmount = 1;
			}
		}
	}
}
