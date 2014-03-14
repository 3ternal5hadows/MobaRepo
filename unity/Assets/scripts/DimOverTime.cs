using UnityEngine;
using System.Collections;

public class DimOverTime : MonoBehaviour {
	public float DimTime=2;
	public float timeoffset=0;
	float timeElapsed;
	float intensity;
	float dimSpeed;	
	// Use this for initialization
	void Start () {
		timeElapsed =timeoffset;
		intensity = this.light.intensity;
		dimSpeed = intensity/DimTime;
	}
	
	// Update is called once per frame
	void Update () {
		timeElapsed+=Time.deltaTime;
		if(timeElapsed<DimTime)
		{
			this.light.intensity -= dimSpeed * Time.deltaTime;
		}else
		{	
			timeElapsed = 0;
			this.light.intensity = intensity;
		}


	}
}
