using UnityEngine;
using System.Collections;

public class DamageObject : MonoBehaviour {
    public int damage;
    public bool canDamageSource;
    public GameObject source;
    public StatusEffects statusEffect;

	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            if ((source == hit.gameObject & canDamageSource) |
                source.GetComponent<PlayerManager>().teamNumber != hit.gameObject.GetComponent<PlayerManager>().teamNumber)
            {
                hit.gameObject.GetComponent<PlayerManager>().TakeDamage(damage, statusEffect);
            }
        }
    }
}
