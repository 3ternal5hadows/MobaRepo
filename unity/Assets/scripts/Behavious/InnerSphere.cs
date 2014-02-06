using UnityEngine;
using System.Collections;

public class InnerSphere : MonoBehaviour {

	// Use this for initialization
    Vector3 rotationVelocity;
    Vector3 lastVelocity;
    
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        rotationVelocity = transform.parent.gameObject.GetComponent<Rigidbody>().velocity; //- lastVelocity;
        //lastVelocity = transform.parent.gameObject.GetComponent<BallMotor>().displacement;
        
        rotationVelocity /= 10;
        

        this.transform.Rotate(rotationVelocity.z, 0, -rotationVelocity.x , Space.World);
        Color color = this.renderer.material.color;
        color.a = Mathf.Clamp((1.0f - (rotationVelocity.magnitude / 10)),0.0f,1.0f); 
        this.renderer.material.color = color;
   
	
	}
}
