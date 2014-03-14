using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

	// Use this for initialization
	public delegate void DestroyAssetsEvent();
	public static event DestroyAssetsEvent DestroyAssets;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
