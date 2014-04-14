using UnityEngine;
using System.Collections;

public class MeleeWeapon : Weapon
{
    public float attackSpeedModifier;
    public float criticalWindowModifier;
    public float specialProcChanceModifier;
    public float specialEffectivenessModifier;
    public float comboDamageModifier;
    public float normalDamageModifier;
    public float powerAttackDamageModifier;
    public float powerAttackCooldownModifier;
    public float powerAttackCastTimeModifier;
    public float statusEffectDamageModifier;

    protected override void WeaponStart()
    {
        base.WeaponStart();
        attackSpeedModifier = GetModifier(4, 7);
        criticalWindowModifier = GetModifier(7, 3);
        specialProcChanceModifier = GetModifier(5, 8);
        specialEffectivenessModifier = GetModifier(5, 1);
        comboDamageModifier = GetModifier(1, 6);
        normalDamageModifier = GetModifier(4, 4);
        powerAttackDamageModifier = GetModifier(4, 9);
        powerAttackCooldownModifier = GetModifier(6, 2);
        powerAttackCastTimeModifier = GetModifier(10, 5);
        statusEffectDamageModifier = GetModifier(4, 0);
    }

    protected override void WeaponUpdate()
    {
        base.WeaponUpdate();
    }

    public override void AttackDown()
    {
        if (normalCooldown.IsOffCooldown)
        {
            gameObject.GetComponent<AttackAnimation>().Attack(player);
        }
        base.AttackDown();
    }

    public override void PowerAttack()
    {
        if (powerCooldown.IsOffCooldown)
        {
            gameObject.GetComponent<AttackAnimation>().PowerAttack(player);
        }
        base.PowerAttack();
    }
}