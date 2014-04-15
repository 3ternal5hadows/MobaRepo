using UnityEngine;
using System.Collections;

public class SpawnOnClick : MonoBehaviour {

	public GameObject ObjectToSpawn;
	public GameObject PowerAttack;
	public Vector3 offset;
	public float cooldown;
	float elapsedTime=0;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if(Input.GetMouseButtonDown(0))Spawn ();
	}
	public void Spawn()
	{
		if(elapsedTime>cooldown)
		{
			GameObject thing = Instantiate(ObjectToSpawn,this.transform.position + offset, Quaternion.identity)as GameObject;
			elapsedTime = 0;
		}
	}
	public void SpawnPower()
	{
		GameObject thing = Instantiate(PowerAttack,this.transform.position + offset, Quaternion.identity)as GameObject;
		elapsedTime = 0;
	}
}
