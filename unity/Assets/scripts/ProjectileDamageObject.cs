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
        if (DataGod.isServer)
        {
            Hit(hit);
        }
    }

    protected override void Hit(Collider hit)
    {
        if (hit.gameObject.tag == "bounds")
        {
            networkView.RPC("DestroyProjectile", RPCMode.Server);
        }
        base.Hit(hit);
    }

    [RPC]
    public void DestroyProjectile()
    {
        Network.Destroy(networkView.viewID);
        GameObject death = Instantiate(deathEffect, this.transform.position, Quaternion.identity) as GameObject;
    }
}
