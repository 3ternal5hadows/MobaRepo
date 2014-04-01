using UnityEngine;
using System.Collections;

//Node Connection Particle by Greg Vincze
public class NodeConnectionParticle : MonoBehaviour {
    public GameObject target;
    private float speed;

	// Use this for initialization
	void Start () {
        speed = 2;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white * 0;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 difference = (target.transform.position - transform.position).normalized;

        transform.position += (difference) * Time.deltaTime * speed;
        if (speed > 1)
        {
            speed -= Time.deltaTime * 3;
        }
        else
        {
            speed -= Time.deltaTime;
        }
        if (speed <= 0)
        {
            speed = 0;
            Destroy(gameObject);
        }
        gameObject.GetComponent<SpriteRenderer>().color = 0.8f * Color.white * Mathf.Sin((speed * 90) * (Mathf.PI / 180.0f));
	}
}
