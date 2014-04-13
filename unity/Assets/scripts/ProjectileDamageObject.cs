using UnityEngine;
using System.Collections;

public class ProjectileDamageObject : DamageObject
{
    public GameObject deathEffect;

    protected override void PlayerHit(Collider hit, PlayerManager sourceObject)
    {
        base.PlayerHit(hit, sourceObject);
        networkView.RPC("DestroyProjectile", RPCMode.Server);
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "bounds")
        {
            GameObject death = Instantiate(deathEffect, this.transform.position, Quaternion.identity) as GameObject;
            networkView.RPC("DestroyProjectile", RPCMode.Server);
        }
        else if (hit.gameObject.tag == "Player")
        {
            PlayerManager sourceObject = GameObject.Find("NetworkManager").GetComponent<NetworkManager>().allPlayers[source];
            if (sourceObject.gameObject != hit.gameObject)
            {
                GameObject death = Instantiate(deathEffect, this.transform.position, Quaternion.identity) as GameObject;
            }
        }
        if (DataGod.isServer)
        {
            Hit(hit);
        }
    }

    [RPC]
    public void DestroyProjectile()
    {
        Network.Destroy(networkView.viewID);
    }
}
