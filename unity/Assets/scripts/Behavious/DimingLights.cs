using UnityEngine;
using System.Collections;

public class DimingLights : MonoBehaviour {

	public float Amplitude = 1;
	public float YShift = 0;
	// Use this for initialization
	public float CycleTime = 5;
	void Start () {
		this.light.intensity = YShift;
	}
	
	// Update is called once per frame
	void Update () {
		this.light.intensity  = Mathf.Sin(Time.timeSinceLevelLoad / CycleTime) * Amplitude + YShift;
	}
}
