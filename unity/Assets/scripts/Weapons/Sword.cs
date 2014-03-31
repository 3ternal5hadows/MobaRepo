using UnityEngine;
using System.Collections;

public class Sword : MeleeWeapon {
    protected override void WeaponStart()
    {
        base.WeaponStart();
        normalCooldown = new Cooldown(0.5f);
        powerCooldown = new Cooldown(5);
    }
    protected override void WeaponUpdate()
    {
        base.WeaponUpdate();
    }
    public override void AttackDown()
    {
        base.AttackDown();
    }
}
