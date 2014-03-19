using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

	int status;

    public GameObject healthPentagon;

	private Vector3 spawnPosition;
	
    //Hey Nic, I made some changes :D
	// Player Attribute variables
    private int health;
    private int maxHealth;
	private float dodgeCoolDown;
	public List<Weapon> toolBelt;
    public int teamNumber;
	
	//{ **Bools for status Effects** 
    private bool burning;
    private bool chilled;
    private bool shocked;
    private bool poisoned;
    private bool blinded;
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
    //public void Burn(float baseDamage){statusEffectsOnPlayer.Add (new BurnEffect(baseDamage));}
    //public void Blind(float duration){statusEffectsOnPlayer.Add(new BlindEffect(duration));}
    //public void Freeze(float duration){statusEffectsOnPlayer.Add(new FrostEffect(duration));}
    //public void Shock(float duration){statusEffectsOnPlayer.Add(new ShockEffect(duration));}
    //public void Poison(float baseDamage){statusEffectsOnPlayer.Add (new BurnEffect(baseDamage));}
	//}
	
	void Start () {
		statusEffectsOnPlayer = new List<StatusEffects>();
        maxHealth = DataGod.PLAYER_MAX_HEALTH;
        health = maxHealth;

        healthPentagon = (GameObject)Instantiate(healthPentagon, transform.position, Quaternion.identity);

        gameObject.GetComponentInChildren<DamageObject>().source = gameObject;

		spawnPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
        healthPentagon.GetComponent<HealthPentagon>().SetPosition(transform.position);
		//updates all statuses on player if they exist
        for (int i = 0; i < statusEffectsOnPlayer.Count; i++)
        {
            StatusEffects status = statusEffectsOnPlayer[i];
            //foreach(StatusEffects status in statusEffectsOnPlayer)
            //{
            status.Update();
            /*if (status is BurnEffect)
            {
                burning = true;
                health -= (int)(status.dps * Time.deltaTime);
            }
            if (status is FrostEffect)
            {
                chilled = true;
            }
            if (status is ShockEffect)
            {
                shocked = true;
            }
            if (status is PoisonEffect)
            {
                poisoned = true;
            }
            if (status is BlindEffect)
            {
                blinded = true;
            }*/
            if (status.Expired())
            {
                statusEffectsOnPlayer.RemoveAt(i);
                i--;
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

    /// <summary>
    /// Removes damage from player's health
    /// </summary>
    /// <param name="damage">The amount of damage</param>
    public void TakeDamage(int damage)
    {
        TakeDamage(damage, null);
    }
    /// <summary>
    /// Removes damage from player's health
    /// </summary>
    /// <param name="damage">The amount of damage</param>
    /// <param name="statusEffect">The Status Effect of the attack</param>
    public void TakeDamage(int damage, StatusEffects statusEffect)
    {
        health -= damage;
        if (statusEffect != null)
        {
            statusEffect.playerScript = this;
            statusEffectsOnPlayer.Add(statusEffect);
        }
        healthPentagon.GetComponent<HealthPentagon>().Show(health, maxHealth);

		if(health <= 0)
		{
			transform.position = spawnPosition;
			health = maxHealth;
		}
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