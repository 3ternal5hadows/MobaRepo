 using UnityEngine;
using System.Collections;

public class throwShuriken : MonoBehaviour {

	public float throwStrength =0;
	public float throwRotationStrength = 0;
	public GameObject clone;
	// Use this for initialization
	void Start () {
	
	}	
	// Update is called once per frame
	void Update () {
	
	}
	public void Throw()
	{
		GameObject shuriken = Instantiate(clone, transform.position, Quaternion.Euler(transform.forward)) as GameObject;
		shuriken.GetComponent<ShurikenScript>().addForce(throwStrength,transform.forward);
		shuriken.GetComponent<ShurikenScript>().addRotationalForce(throwRotationStrength,16);
		
		shuriken.GetComponent<ShurikenScript>().player = this.gameObject.transform.parent.parent.parent.gameObject;
	}

}
