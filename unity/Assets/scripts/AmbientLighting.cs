using UnityEngine;
using System.Collections;

public class AmbientLighting : MonoBehaviour {

	// Use this for initialization
	public float MaxIntensity = 1;
	public float MinIntensity = 0;
	
	public float CycleTime = 5;
	void Start () {
		this.light.intensity = MinIntensity;
	
	}
	
	// Update is called once per frame
	void Update () {
		this.light.intensity  = Mathf.Sin(Time.timeSinceLevelLoad / CycleTime) * MaxIntensity + MinIntensity;
 	
	}
}
