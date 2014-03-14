using UnityEngine;
using System.Collections;

public class AssetsDestroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EventManager.DestroyAssets += DestroyAssets;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void DestroyAssets()
	{
		Destroy(gameObject.GetComponent<MeshRenderer>());
		Destroy(gameObject.GetComponent<MeshFilter>());
		//Destroy(gameObject.GetComponent<Material>());
	}
}
