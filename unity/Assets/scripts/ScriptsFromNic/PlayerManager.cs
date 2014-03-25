using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

	int status;

    public GameObject healthPentagon;

	public Vector3 spawnPosition;
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
    public int killer;
    private ScoreKeeper scoreKeeper;
    private NetworkManager networkManager;
    public string name;

	public List<StatusEffects> statusEffectsOnPlayer;

    public int playerNumber;

    public GameObject allyMarker;

    void Start()
    {
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        networkManager.allPlayers.Add(this);

        allyMarker = transform.FindChild("AllyMarker").gameObject;

        statusEffectsOnPlayer = new List<StatusEffects>();
        healthPentagon = (GameObject)Instantiate(healthPentagon, transform.position, Quaternion.identity);

        if (networkView.isMine)
        {
            networkView.RPC("SetPlayerNumber", RPCMode.AllBuffered, playerNumber, teamNumber);
        }
        if (DataGod.isServer)
        {
            spawnPosition = transform.position;
            respawnTimer = new Timer(DataGod.PLAYER_RESPAWN_TIME);
            maxHealth = DataGod.PLAYER_MAX_HEALTH;
            health = maxHealth;
            name = DataGod.GetRandomName();
            for (int i = 0; i < networkManager.allPlayers.Count; i++)
            {
                networkManager.allPlayers[i].networkView.RPC("SendData", RPCMode.All, networkManager.allPlayers[i].name, networkManager.allPlayers[i].health, networkManager.allPlayers[i].maxHealth, networkManager.allPlayers[i].spawnPosition);
            }
        }

        scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
    }

    [RPC]
    public void SendData(string name, int health, int maxHealth, Vector3 spawnPosition)
    {
        this.name = name;
        this.health = health;
        this.maxHealth = maxHealth;
        this.spawnPosition = spawnPosition;
        if (networkView.isMine)
        {
            ChatManager chat = GameObject.Find("ChatManager").GetComponent<ChatManager>();
            chat.playerName = name;
            chat.player = this;
        }
    }
	
	// Update is called once per frame
    void Update()
    {
        if (DataGod.isServer)
        {
            if (health <= 0)
            {
                respawnTimer.Update();
                if (respawnTimer.HasCompleted())
                {
                    Respawn();
                }
            }
        }
        if (networkView.isMine)
        {
            for (int i = 0; i < networkManager.allPlayers.Count; i++)
            {
                if (networkManager.allPlayers[i].teamNumber != teamNumber)
                {
                    networkManager.allPlayers[i].allyMarker.SetActive(false);
                }
                else
                {
                    networkManager.allPlayers[i].allyMarker.SetActive(true);
                }
            }
            
            //else
            //{
                //updates all statuses on player if they exist
                //for (int i = 0; i < statusEffectsOnPlayer.Count; i++)
                //{
                //    StatusEffects status = statusEffectsOnPlayer[i];
                //    status.Update();
                //    if (status.Expired())
                //    {
                //        statusEffectsOnPlayer.RemoveAt(i);
                //        i--;
                //    }
                //}
            //}
        }

        healthPentagon.GetComponent<HealthPentagon>().SetPosition(transform.position);
        if (DataGod.isServer)
        {
            healthPentagon.GetComponent<HealthPentagon>().Show(health, maxHealth);
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
    public void TakeDamage(int damage, int source, bool showHealthPentagon = true)
    {
        TakeDamage(damage, source, null, showHealthPentagon);
    }
    /// <summary>
    /// Removes damage from player's health
    /// </summary>
    /// <param name="damage">The amount of damage</param>
    /// <param name="statusEffect">The Status Effect of the attack</param>
    public void TakeDamage(int damage, int source, StatusEffects statusEffect, bool showHealthPentagon = true)
    {
        if (DataGod.isServer)
        {
            if (health > 0)
            {
                health -= damage;
                networkView.RPC("RPCTakeDamage", RPCMode.All, damage, health, source, (showHealthPentagon) ? 1 : 0);
                //if (statusEffect != null)
                //{
                //    statusEffect.playerScript = this;
                //    statusEffect.sourceScript = networkManager.allPlayers[source];
                //    statusEffectsOnPlayer.Add(statusEffect);
                //}
            }
        }
    }

    [RPC]
    public void RPCTakeDamage(int damage, int newHealth, int source, int showHealthPentagon)
    {
        health = newHealth;

        if (health <= 0)
        {
            health = 0;
            killer = source;
            transform.position = GameObject.Find("DeathSpawn").transform.position;
            if (networkView.isMine)
            {
                gameObject.GetComponent<HUD>().showDeathInfo = true;
            }
            if (DataGod.isServer)
            {
                networkManager.allPlayers[killer].networkView.RPC("RPCAddKill", RPCMode.AllBuffered);
                networkView.RPC("RPCAddDeath", RPCMode.AllBuffered);
                scoreKeeper.networkView.RPC("RPCAddKill", RPCMode.AllBuffered, networkManager.allPlayers[killer].teamNumber);
            }
        }
        if (showHealthPentagon != 0 & (!networkView.isMine))
        {
            healthPentagon.GetComponent<HealthPentagon>().Show(health, maxHealth);
        }
    }

    [RPC]
    public void RPCAddKill()
    {
        kills++;
    }

    [RPC]
    public void RPCAddDeath()
    {
        deaths++;
    }

    private void Respawn()
    {
        networkView.RPC("RPCRespawn", RPCMode.All);
    }

    [RPC]
    public void RPCRespawn()
    {
        if (networkView.isMine)
        {
            gameObject.GetComponent<HUD>().showDeathInfo = false;
        }
        health = maxHealth;
        transform.position = spawnPosition;
    }

    /// <summary>
    /// Returns the player's health as a number between 0 and 1
    /// </summary>
    /// <returns>a number between 0 and 1</returns>
    public float GetHealthPercentage()
    {
        return ((float)health / (float)maxHealth);
    }

    [RPC]
    public void SetPlayerNumber(int number, int teamNumber)
    {
        playerNumber = number;
        this.teamNumber = teamNumber;
        gameObject.GetComponentInChildren<ProjectileLauncher>().source = playerNumber;
        gameObject.GetComponentInChildren<DamageObject>().source = playerNumber;
    }
}