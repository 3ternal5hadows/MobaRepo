using UnityEngine;
using System.Collections;

public class SpawnTimer : MonoBehaviour {

	// Use this for initialization
	public GameObject spawn;
	public float spawnTime=5;
	float elapsedTime;
	void Start () {
		elapsedTime =0;
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if(elapsedTime>=spawnTime)
		{
			elapsedTime =0;
			Instantiate(spawn,this.transform.position, Quaternion.identity);
		}
	
	}
}
