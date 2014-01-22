using UnityEngine;
using System.Collections;

public class LightWaypoint : MonoBehaviour {

	
	public GameObject WayPoint1;
	public GameObject WayPoint2;
	public GameObject WayPoint3;
	public GameObject WayPoint4;
	public GameObject WayPoint5;
	
	public int StartingWapoint = 1;	
	Transform currentDirection;
	
	Vector3 Velocity;
	Vector3 Acceleration;
	public float speed = 10;
	
 	public int waypoint;
	
	void Start () {
		waypoint = StartingWapoint;
	}
	
	
	void Update () {
		switch(waypoint)
		{
			case 1:
				currentDirection = WayPoint1.transform;
				break;
			case 2:
				currentDirection = WayPoint2.transform;
				break;
			case 3:
				currentDirection = WayPoint3.transform;
				break;
			case 4:
				currentDirection = WayPoint4.transform;
				break;
			case 5:
				currentDirection = WayPoint5.transform;
				break;	
		
		}
		Vector3 direction = currentDirection.transform.position - this.transform.position;
		
		this.transform.Translate(direction.normalized * speed);
		
	
	}
	void OnTriggerEnter(Collider hit)
	{
		if(hit.gameObject.tag == "Waypoint")
		{
			if(waypoint!=5)
			{
				waypoint++;
			}else waypoint = 1;			
		}
	}
}
