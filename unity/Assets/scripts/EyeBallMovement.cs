using UnityEngine;
using System.Collections;

public class EyeBallMovement : MonoBehaviour {

	// Use this for initialization
    float movementSpeed = 0.3f;
    public Transform waypoint;
    bool hitWaypoint;
    

	void Start () {
        hitWaypoint = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (hitWaypoint)
        {
            this.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
            Debug.Log(waypoint.ToString());
            transform.Translate(transform.forward * movementSpeed);
        }
        else
        {
            
            this.transform.LookAt(waypoint);
            transform.Translate(transform.forward * movementSpeed);
        }

        
	    
	}
    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.name == "WayPoint")
        {
            Debug.Log("Waypoint hit");
            hitWaypoint = true;
        }
    }
}
