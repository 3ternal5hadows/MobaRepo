using UnityEngine;
using System.Collections;

public class shurikenPowerThrow : MonoBehaviour {
	
	public float throwStrength =0;
	public float throwRotationStrength = 0;
	public GameObject empty;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			GameObject shurikenEmpty = Instantiate(empty, transform.position, Quaternion.Euler(transform.forward)) as GameObject;
			shurikenEmpty.GetComponent<ShurikenEmptyScript>().addForce(throwStrength,transform.forward);
			
			shurikenEmpty.GetComponent<ShurikenEmptyScript>().player = this.gameObject;
		}
	}
}