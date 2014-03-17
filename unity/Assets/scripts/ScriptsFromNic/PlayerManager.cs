using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

	int status;
	
	// Player Attribute variables
	public float health;
	public float dodgeCoolDown;
	public List<Weapon> toolBelt;
	
	//{ **Bools for status Effects** 
	public bool burning;
	public bool chilled;
	public bool shocked;
	public bool poisoned;
	public bool blinded;
	//}
	public List<StatusEffects> statusEffectsOnPlayer;
	
	/*elemental effects
		0 = Burn
		1 = Blind
		2 = Freeze
		3 = Shock
		4 = Poison
	*/
	
	//{ **Methods for adding Status Effect to Players**
	public void Burn(float baseDamage){statusEffectsOnPlayer.Add (new BurnEffect(baseDamage));}
	public void Blind(float duration){statusEffectsOnPlayer.Add(new BlindEffect(duration));}
	public void Freeze(float duration){statusEffectsOnPlayer.Add(new FrostEffect(duration));}
	public void Shock(float duration){statusEffectsOnPlayer.Add(new ShockEffect(duration));}
	public void Poison(float baseDamage){statusEffectsOnPlayer.Add (new BurnEffect(baseDamage));}
	//}
	
	void Start () {
		statusEffectsOnPlayer = new List<StatusEffects>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//updates all statuses on player if they exist
		foreach(StatusEffects status in statusEffectsOnPlayer)
		{
			status.Update();
			if(status is BurnEffect)
			{
				burning = true;
				health -= status.dps * Time.deltaTime;
			}
			if(status is FrostEffect)
			{
				chilled = true;
			}
			if(status is ShockEffect)
			{
				shocked = true;
			}
			if(status is PoisonEffect)
			{
				poisoned = true;
			}
			if(status is BlindEffect)
			{
				blinded = true;
			}
		}

	}
	public void Attack()
	{
		//if left hand
		//if right hand
	}
	public void loadWeapon()
	{
		//loads weapon of type into empty weapon slot
	}
}
public class Weapon : MonoBehaviour
{
	//skillTree Effects on all weapons

	public float atkSpd;
	public float pwrAtkActivateSpd;
	public float pwrtAtkCoolDown;
	public float pwrAtkDmg;
	public float nrmAtkDmg;
	public float critDmgBns;
	public float specEffectivePrcnt;
	public float procChnce;
	
}
public class Melee:Weapon
{
	public float critWindowRnge;
	public enum elemType{shadow,fire,ice,lightning,poison};
	elemType element;
}
public class Ranged:Weapon
{
	public float chnceToNotConsumeCombo;
	public enum elemType{shadow,fire,ice,lightning,poison};
	elemType element;
}
public class Spell:Weapon
{
	public float comboTmrLngth;

	public enum elemType{shadow,fire,ice,lightning};
	elemType primary;
	elemType secondary;

	public bool didItCrit()
	{
		return procChnce>=Random.Range (0.0f,100.0f);
	}
}