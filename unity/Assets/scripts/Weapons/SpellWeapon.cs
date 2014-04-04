using UnityEngine;
using System.Collections;

public class SpellWeapon : Weapon
{
	public float normalCooldownTime;
	private Cooldown normalCooldown;
	
	public float attackSpeedModifier;
	
	public float specialProcChanceModifier;
	public float specialEffectivenessModifier;
	public float comboDamageModifier;
	public float normalDamageModifier;
	public float powerAttackDamageModifier;
	public float powerAttackCooldownModifier;
	public float powerAttackCastTimeModifier;
	public float statusEffectDamageModifier;
	
	public float comboTmrLength;
	//public enum elemType { shadow, fire, ice, lightning, poison };
	//elemType element;
	private ProjectileLauncher launcher;
	
	protected override void WeaponStart()
	{
		base.WeaponStart();
		launcher = gameObject.GetComponent<ProjectileLauncher>();
		attackSpeedModifier = GetModifier(4, 7);
		comboTmrLength = GetModifier(7, 3);
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
		launcher.Create();
	}
	
	public override void AttackUp()
	{
		launcher.Launch();
	}
}