using UnityEngine;
using System.Collections;

public class DoorColliderShutOff : MonoBehaviour {

	// Use this for initialization
	DissolvingDoor doorScript;

	void Start () {
		doorScript = transform.parent.gameObject.GetComponent<DissolvingDoor>();
	}
	
	// Update is called once per frame
	void Update () {
		if(doorScript.sliceAmount>0.9f)
		{
			this.gameObject.SetActive(false);
		}else if(this.gameObject.activeSelf)this.gameObject.SetActive(true);
		
	}
}
