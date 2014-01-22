using UnityEngine;
using System.Collections;

public class TurretAiRotation : MonoBehaviour {

   	public bool playerWithinRange = false;
    Vector3 playerPosition;
	void Start () {
        playerWithinRange = false;
	}
	
	// Update is called once per frame
	void Update () {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (playerWithinRange)
        {
            this.transform.LookAt(new Vector3( playerPosition.x,playerPosition.y,playerPosition.z));
        }
	}
    void OnTriggerStay(Collider Hit)
    {
        if (Hit.gameObject.name == "TankBody")
        {
	            playerWithinRange = true;
        }
    }
	void OnTriggerExit(Collider Hit)
	{
		if (Hit.gameObject.name == "TankBody")
        {
	            playerWithinRange = false;
        }
	}
}
