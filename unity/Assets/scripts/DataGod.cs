using UnityEngine;
using System.Collections;

public class DataGod
{
    public static bool isClient;
    public static bool isServer
    {
        get
        {
            return !isClient;
        }
    }
    public static bool networkIsMine = false;
    public enum GameMode { Menu, Demo, NetWorkPlay };
    public static GameMode currentGameState;

    //Gameplay constants
    public const float CRIT_MULTIPLIER = 2;

    //Talent tree variables
    public const int POINTS_REQUIRED_FOR_NEXT_NODE = 5;
    public const int MAXTALENTPOINTS = 50;
    public static int talentPoints;
    public const int MAXIMUM_NODE_LEVEL = 15;
    //Specific Talent tree variables
    //public static int[,] skillTreeData = new int[3, 10];

    //Player constants
    public const int PLAYER_MAX_HEALTH = 100000;
    public const int PLAYER_RESPAWN_TIME = 7;
    //Player variables

    public static string playerName = "Default Name";

    private static string[] defaultNames = new string[] { "Greg", "Nic", "Wes", "Brad", "Michael",
        "Andrew", "Bob", "Catherine", "Delilah", "Elizabeth", "Fred", "George", "Hubert", "Ingred", "Julia",
    "Kyle", "Liam", "Maria", "Nancy", "Odin", "Patrick", "Quinn", "Riley", "Samantha", "Thomas", "Umer", "Victoria",
    "Witchita", "Xavior", "Yawn", "Zachary", "Baby Jesus", "Helix"};
    public static string GetRandomName()
    {
        return defaultNames[Random.Range(0, defaultNames.Length)];
    }

    //Chat filter
    #region Not for the faint of heart
    public static string[] wordsWeDontLike = new string[] { "tank", "t@nk", "artillery", "op" };
    #endregion
    private static string[] filterAlternateMessages = new string[] { "I like bunny rabbits", "This winter is too long",
    "I'm such a noob", "Look it's a spider", "Mmm, that tasted purple", "What did I just say"};
    public static string GetRandomMessage()
    {
        return filterAlternateMessages[Random.Range(0, filterAlternateMessages.Length)];
    }

    public static string[] treeToolTips
    {
        get
        {
            return new string[]
    {
        "Element damage and duration\n+4% damage per point\n+4% duration per point",
		"Special Effectiveness % increase\n+5% effectiveness per point",
		"Power Attack Cooldown\n+1% cooldown",
        "Leave this blank, fill in the one below",
		"Normal Attack Damage\n+2% damage per point",
		"Power Attack Activation Speed\n+1% activation speed per point",
		"Crit/Combo Damage Bonus\n+1% damage per point",
		"Attack Speed Increase\n+1% attack speed per point",
		"Special Pro Chance Per Combo\n+10% chance per point",
		"Power Attack Damage\n+3% damage per point"
			};
        }
    }

    public static string[] treeToolTipsID3
    {
        get
        {
            return new string[]
    {
        "Critical Window Range/Width\n+0.02 seconds per point",
        "Chance to not Consume Combo\n+5%",
        "Combo Timer Length\n+0.5 seconds per point"
    };
        }
    }
}
