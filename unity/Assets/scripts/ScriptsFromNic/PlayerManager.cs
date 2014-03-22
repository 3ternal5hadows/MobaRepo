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
    //Kill/Death
    public int kills;
    public int deaths;
    //The player who killed this player
    public PlayerManager killer;
    public string name;

	public List<StatusEffects> statusEffectsOnPlayer;

    void Start()
    {
        gameObject.GetComponentInChildren<ProjectileLauncher>().source = gameObject;
        statusEffectsOnPlayer = new List<StatusEffects>();
        maxHealth = DataGod.PLAYER_MAX_HEALTH;
        health = maxHealth;
        healthPentagon = (GameObject)Instantiate(healthPentagon, transform.position, Quaternion.identity);
        spawnPosition = transform.position;
        respawnTimer = new Timer(DataGod.PLAYER_RESPAWN_TIME);

        if (networkView.isMine)
        {
            name = DataGod.GetRandomName();
            ChatManager chat = GameObject.Find("ChatManager").GetComponent<ChatManager>();
            chat.playerName = name;
            chat.player = this;

            networkView.RPC("SendData", RPCMode.AllBuffered, name);
        }
    }

    [RPC]
    public void SendData(string name)
    {
        this.name = name;
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
                status.Update();
                if (status.Expired())
                {
                    statusEffectsOnPlayer.RemoveAt(i);
                    i--;
                }
            }
        }
    }
    /// <summary>
    /// Checks if the player is stunned
    /// </summary>
    /// <returns>Returns true if stunned, false if not</returns>
    public bool IsStunned()
    {
        foreach (StatusEffects status in statusEffectsOnPlayer)
        {
            if (status is ShockEffect)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Calculates the total slow on the player, returns a number that the player's speed should be divided by
    /// </summary>
    /// <returns>returns a number that the player's speed should be divided by</returns>
    public float GetTotalSlows()
    {
        float slowPercentage = 0;
        foreach (StatusEffects status in statusEffectsOnPlayer)
        {
            if (status is FrostEffect)
            {
                slowPercentage += ((FrostEffect)status).slowPercentage;
            }
        }
        return 1 + slowPercentage;
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
                if (!networkView.isMine)
                {
                    healthPentagon.GetComponent<HealthPentagon>().Show(health, maxHealth);
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

