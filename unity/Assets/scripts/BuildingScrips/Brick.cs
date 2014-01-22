using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	// Use this for initialization
    float alpha;

	void Start () {
        this.transform.rigidbody.Sleep();
		this.transform.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        alpha = this.renderer.material.color.a;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (this.transform.parent.gameObject.GetComponent<Building>().health > 0)
        {
            this.transform.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
			this.transform.rigidbody.WakeUp();
            this.transform.rigidbody.constraints = RigidbodyConstraints.None;
            if (alpha > 0)
            {
                alpha -= 0.007f;
                Color color = renderer.material.color;                
                color.a = alpha;                
                this.renderer.material.color = color;
            }
            else Destroy(this.gameObject);
        }
    }
}
