using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {
	
	public Vector3 startingPoint;
	private int lives;
	// Use this for initialization
	void Start () {
		
		startingPoint = new Vector3(this.transform.position.x,
									this.transform.position.y,
									this.transform.position.z);
		lives = 3;
		Debug.Log("Number of Lives start "+ lives);	
		
			
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(this.transform.position);
		if(lives>0)
		{
			if(this.transform.position.y<-5)
			{
				lives-=1;
				Debug.Log("Number of Lives "+ lives +" "+gameObject.name);
				
				this.transform.position = startingPoint;
			}
		}
		else if(lives == 0 && this.transform.position.y<-5)
		{ 
			Application.Quit();
		}
	}
}
