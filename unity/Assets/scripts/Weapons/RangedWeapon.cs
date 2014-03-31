using UnityEngine;
using System.Collections;

public class RangedWeapon : Weapon
{
    public float normalCooldownTime;
    private Cooldown normalCooldown;

    public float chnceToNotConsumeCombo;
    //public enum elemType { shadow, fire, ice, lightning, poison };
    //elemType element;
    private ProjectileLauncher launcher;

    protected override void WeaponStart()
    {
        base.WeaponStart();
        launcher = gameObject.GetComponent<ProjectileLauncher>();
    }

    protected override void WeaponUpdate()
    {
        base.WeaponUpdate();
    }

    public override void AttackDown()
    {
        launcher.Create();
    }

    public override void AttackUp()
    {
        launcher.Launch();
    }
}