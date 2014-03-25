using UnityEngine;
using System.Collections;

public class ProjectileDamageObject : DamageObject {
    public GameObject deathEffect;

    protected override void PlayerHit(Collider hit, PlayerManager sourceObject)
    {
        base.PlayerHit(hit, sourceObject);
        networkView.RPC("DestroyProjectile", RPCMode.Server);
    }

    void OnTriggerEnter(Collider hit)
    {
        if (DataGod.isServer)
        {
            Hit(hit);
        }
    }

    [RPC]
    public void DestroyProjectile()
    {
        if (DataGod.isServer)
        {
            //GameObject death = Network.Instantiate(deathEffect, this.transform.position, Quaternion.identity, 0) as GameObject;
            //Network.Destroy(gameObject);
            //Network.Destroy(networkView.viewID);
        }
        Network.Destroy(networkView.viewID);
        GameObject death = Instantiate(deathEffect, this.transform.position, Quaternion.identity) as GameObject;
        //Destroy(gameObject);
    }
}
