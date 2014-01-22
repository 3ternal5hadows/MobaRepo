using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

	// Use this for initialization
	
	public GameObject EnemyType;
	public int AmountOfEnemiesPerWave;
	public int AmountOfWaves;
	public float TimeBetweenWaves;	
	public float spawnRate;
	float currentTime;
    float waveTime;
    float enemiesSpawnedThisWave;

	
	float health;
	bool awake;
	void Start () {
		awake = false;
        currentTime = spawnRate;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(awake)
		{
			currentTime += Time.deltaTime;
            waveTime += Time.deltaTime;
			if(currentTime >= spawnRate && AmountOfEnemiesPerWave > enemiesSpawnedThisWave)
			{
				GameObject entity;
				entity = Instantiate(EnemyType,this.transform.position, Quaternion.identity) as GameObject;

                entity.GetComponent<EyeBallMovement>().waypoint = GetComponentInChildren<WaypointLocation>().transform;
                enemiesSpawnedThisWave++;
				currentTime = 0;
			}
            if (waveTime >= TimeBetweenWaves)
            {
                enemiesSpawnedThisWave = 0;
                waveTime = 0;
            }
		}
		
	}
	void OnTriggerEnter(Collider hit)
	{
		if(!awake)
		{
			if(hit.gameObject.name == "TankBody")
			{
				wakeUpSpawner();
			}
		}
	}
	void wakeUpSpawner()
	{
        Debug.Log("spawner Awake");
		awake = true;
		
	}
    void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.name == "TankBody")
        {
            awake = false;
        }
    }
}
