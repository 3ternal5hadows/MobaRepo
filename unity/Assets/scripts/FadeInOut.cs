using UnityEngine;
using System.Collections;

public class FadeInOut : MonoBehaviour {

	public float DimSpeed=1;
	public bool ResetAtZero=true;
	float elapsedTime;
	float intensity;


	// Use this for initialization
	void Start () {
		elapsedTime =0;
		intensity = light.intensity;
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += (Mathf.PI*2*Time.deltaTime)*DimSpeed	;
		if(Mathf.Sin (elapsedTime)<0)
		{
			elapsedTime =0;
		}
		light.intensity = intensity * Mathf.Sin (elapsedTime);

	}
}
