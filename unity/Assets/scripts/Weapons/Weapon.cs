using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public StatusEffectInfo element;
    public int ID;
    private bool equipped;
    public bool Equipped
    {
        get { return equipped; }
        set
        {
            equipped = value;
            if (gameObject.GetComponent<MeshRenderer>() != null)
            {
                if (renderer == null)
                {
                    renderer = gameObject.GetComponent<MeshRenderer>();
                }
                renderer.enabled = equipped;
            }
        }
    }
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
        if (networkView.isMine)
        {
            networkView.RPC("RPCEquipped", RPCMode.All, equipped);
        }
    }
    protected virtual void WeaponUpdate() { }

    public virtual void AttackDown()
    {
        networkView.RPC("ServerAttack", RPCMode.Server);
        normalCooldown.GoOnCooldown();
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
        powerCooldown.GoOnCooldown();
    }

    [RPC]
    public void RPCEquipped(bool equipped)
    {
        Equipped = equipped;
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