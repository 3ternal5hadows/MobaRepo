using UnityEngine;
using System.Collections;

public class ServerLight : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (DataGod.isClient)
        {
            gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
