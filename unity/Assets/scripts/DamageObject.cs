using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageObject : MonoBehaviour
{
    public int damage;
    public bool canDamageSource;
    public int source;
    public StatusEffects statusEffect;
    private List<GameObject> enemiesHit;

    // Use this for initialization
    void Start()
    {
        enemiesHit = new List<GameObject>();
    }
    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider hit)
    {
        if (DataGod.isServer)
        {
            Hit(hit);
        }
    }

    [RPC]
    public void RPCAttack()
    {
        enemiesHit.Clear();
    }

    protected virtual void Hit(Collider hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            if (!HitAlready(hit.gameObject))
            {
                PlayerManager sourceObject = GameObject.Find("NetworkManager").GetComponent<NetworkManager>().allPlayers[source];
                if ((sourceObject.gameObject == hit.gameObject & canDamageSource) |
                    sourceObject.teamNumber != hit.gameObject.GetComponent<PlayerManager>().teamNumber)
                {
                    PlayerHit(hit, sourceObject);
                }
                enemiesHit.Add(hit.gameObject);
            }
        }
    }

    private bool HitAlready(GameObject playerHit)
    {
        foreach (GameObject player in enemiesHit)
        {
            if (playerHit == player)
            {
                return true;
            }
        }
        return false;
    }

    protected virtual void PlayerHit(Collider hit, PlayerManager sourceObject)
    {
        hit.gameObject.GetComponent<PlayerManager>().TakeDamage(damage, sourceObject.playerNumber, statusEffect);
    }

    [RPC]
    public void RPCSource(int source)
    {
        this.source = source;
    }
}
