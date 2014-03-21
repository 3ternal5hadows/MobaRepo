using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

	int status;

    public GameObject healthPentagon;

	private Vector3 spawnPosition;
    private Timer respawnTimer;
	
    //Hey Nic, I made some changes :D
	// Player Attribute variables
    private int health;
    private int maxHealth;
	private float dodgeCoolDown;
	public List<Weapon> toolBelt;
    public int teamNumber;
    //KDA
    public int kills;
    public int deaths;
    //The player who killed this player
    public PlayerManager killer;
    public string name;
	
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
        gameObject.GetComponentInChildren<ProjectileLauncher>().source = gameObject;
		statusEffectsOnPlayer = new List<StatusEffects>();
        maxHealth = DataGod.PLAYER_MAX_HEALTH;
        health = maxHealth;

        if (networkView != null)
        {
            healthPentagon = (GameObject)Instantiate(healthPentagon, transform.position, Quaternion.identity);
        }

		spawnPosition = transform.position;

        respawnTimer = new Timer(DataGod.PLAYER_RESPAWN_TIME);

        name = DataGod.GetRandomName();
	}
	
	// Update is called once per frame
    void Update()
    {
        healthPentagon.GetComponent<HealthPentagon>().SetPosition(transform.position);

        if (health <= 0)
        {
            respawnTimer.Update();
            if (respawnTimer.HasCompleted())
            {
                Respawn();
            }
        }
        else
        {
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
    public void TakeDamage(int damage, PlayerManager source, bool showHealthPentagon = true)
    {
        TakeDamage(damage, source, null, showHealthPentagon);
    }
    /// <summary>
    /// Removes damage from player's health
    /// </summary>
    /// <param name="damage">The amount of damage</param>
    /// <param name="statusEffect">The Status Effect of the attack</param>
    public void TakeDamage(int damage, PlayerManager source, StatusEffects statusEffect, bool showHealthPentagon = true)
    {
        if (health > 0)
        {
            health -= damage;
            if (statusEffect != null)
            {
                statusEffect.playerScript = this;
                statusEffect.sourceScript = source;
                statusEffectsOnPlayer.Add(statusEffect);
            }
            if (showHealthPentagon)
            {
                if (networkView != null)
                {
                    if (!networkView.isMine)
                    {
                        healthPentagon.GetComponent<HealthPentagon>().Show(health, maxHealth);
                    }
                }
            }

            if (health <= 0)
            {
                killer = source;
                gameObject.GetComponent<HUD>().showDeathInfo = true;
                killer.kills++;
                deaths++;
            }
        }
    }

    private void Respawn()
    {
        transform.position = spawnPosition;
        health = maxHealth;
        gameObject.GetComponent<HUD>().showDeathInfo = false;
    }
    /// <summary>
    /// Returns the player's health as a number between 0 and 1
    /// </summary>
    /// <returns>a number between 0 and 1</returns>
    public float GetHealthPercentage()
    {
        return ((float)health / (float)maxHealth);
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