using UnityEngine;
using System.Collections;

public class DataGod {
    public static bool isClient;
	public static bool networkIsMine = false;
	public enum GameMode{ Menu, Demo, NetWorkPlay};	
	public static GameMode currentGameState;

    //Talent tree variables
    public const int POINTS_REQUIRED_FOR_NEXT_NODE = 5;
    public const int MAXTALENTPOINTS = 50;
    public static int talentPoints = MAXTALENTPOINTS;
    public const int MAXIMUM_NODE_LEVEL = 5;
    //Specific Talent tree variables
    public static int[,] skillTreeData = new int[3, 10];

    //Player constants
    public const int PLAYER_MAX_HEALTH = 100000;
    public const int PLAYER_RESPAWN_TIME = 5;

    private static string[] defaultNames = new string[] { "Greg", "Nic", "Wes", "Brad", "Michael",
        "Andrew", "Bob", "Catherine", "Delilah", "Elizabeth", "Fred", "George", "Hubert", "Ingred", "Julia",
    "Kyle", "Liam", "Maria", "Nancy", "Odin", "Patrick", "Quinn", "Riley", "Samantha", "Thomas", "Umer", "Victoria",
    "Witchita", "Xavior", "Yawn", "Zachary"};
    public static string GetRandomName()
    {
        return defaultNames[Random.Range(0, defaultNames.Length)];
    }
}
