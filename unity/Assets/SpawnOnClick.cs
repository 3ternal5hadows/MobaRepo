using UnityEngine;
using System.Collections;

public class SpawnOnClick : MonoBehaviour {

	public GameObject ObjectToSpawn;
	public Vector3 offset;
	public float cooldown;
	float elapsedTime=0;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
	}
	public void Spawn()
	{
		if(elapsedTime>cooldown)
		{
			GameObject thing = Instantiate(ObjectToSpawn,this.transform.position + offset, Quaternion.identity)as GameObject;
			elapsedTime = 0;
		}
	}
}
