using UnityEngine;
using System.Collections;

public class Dagger : MeleeWeapon {
	protected override void WeaponStart()
	{

		normalCooldown = new Cooldown(0.5f);
		powerCooldown = new Cooldown(5);
		base.WeaponStart();
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
