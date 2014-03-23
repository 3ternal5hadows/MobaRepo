﻿using UnityEngine;
using System.Collections;

public class DamageObject : MonoBehaviour {
    public int damage;
    public bool canDamageSource;
    public int source;
    public StatusEffects statusEffect;

	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter(Collider hit)
    {
        if (DataGod.isServer)
        {
            if (hit.gameObject.tag == "Player")
            {
                PlayerManager sourceObject = GameObject.Find("NetworkManager").GetComponent<NetworkManager>().allPlayers[source];
                if ((sourceObject.gameObject == hit.gameObject & canDamageSource) |
                    sourceObject.teamNumber != hit.gameObject.GetComponent<PlayerManager>().teamNumber)
                {
                    hit.gameObject.GetComponent<PlayerManager>().TakeDamage(damage, sourceObject.playerNumber, statusEffect);
                }
            }
        }
    }

    [RPC]
    public void RPCSource(int source)
    {
        this.source = source;
    }
}
