using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public StatusEffectInfo element;
    public int ID;
    private MeshRenderer renderer;
    public Cooldown normalCooldown;
    public Cooldown powerCooldown;
    public Sprite icon;
    public PlayerManager player;

    protected void Start()
    {
        WeaponStart();
    }

    protected void Update()
    {
        WeaponUpdate();
    }

    protected virtual void WeaponStart()
    {
    }
    protected virtual void WeaponUpdate()
    {
        if (normalCooldown != null)
        {
            normalCooldown.Update();
            powerCooldown.Update();
        }
    }

    public virtual void AttackDown()
    {
        if (normalCooldown.IsOffCooldown)
        {
            networkView.RPC("ServerAttack", RPCMode.Server);
            networkView.RPC("RPCNormalCooldown", RPCMode.All);
        }
    }

    public virtual void AttackUp()
    {
    }

    [RPC]
    public void ServerAttack()
    {
        gameObject.GetComponent<DamageObject>().ResetAttack();
    }

    public virtual void PowerAttack()
    {
        networkView.RPC("RPCPowerCooldown", RPCMode.All);
    }

    [RPC]
    public void RPCNormalCooldown()
    {
        normalCooldown.GoOnCooldown();
    }

    [RPC]
    public void RPCPowerCooldown()
    {
        powerCooldown.GoOnCooldown();
    }
    [RPC]
    public void SetParent(NetworkViewID viewId)
    {
        transform.parent = NetworkView.Find(viewId).transform;
    }

    protected float GetModifier(int percentPerPoint, int nodeID)
    {
        return 1 + (percentPerPoint * WeaponData.treeData[ID, nodeID]) / 100.0f;
    }

    public StatusEffects GetStatusEffect()
    {
        if (element != null)
        {
            if (Random.Range(0, 100) / 100.0f <= element.chanceToApply)
            {
                return element.statusEffect.GetNewEffect();
            }
        }
        return null;
    }
}