using UnityEngine;
using System.Collections;

public class HealthGUI : MonoBehaviour {

	// Use this for initialization
	float health;
	float time;
	
	void Start () {
		health = Mathf.Round(GameObject.FindGameObjectWithTag("Player").GetComponent<TankMovement>().Health);
		guiText.text = "Health:" + health.ToString();
	
	}
	
	// Update is called once per frame
	void Update () {
	
		health = Mathf.Round(GameObject.FindGameObjectWithTag("Player").GetComponent<TankMovement>().Health);
		if(health < 200)
		{
			time = Mathf.Sin(Time.timeSinceLevelLoad*24);
		}else time = 1;
		
		guiText.color = new Color((1000-health)/1000, health/1000,0, time);
		
		
		guiText.text = "Health:" + health.ToString();
	
	}
}
