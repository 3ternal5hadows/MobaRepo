﻿using UnityEngine;
using System.Collections;

public class DataGod {
    public static bool isClient;
    public static bool isServer
    {
        get
        {
            return !isClient;
        }
    }
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
    public const int PLAYER_RESPAWN_TIME = 7;

    private static string[] defaultNames = new string[] { "Greg", "Nic", "Wes", "Brad", "Michael",
        "Andrew", "Bob", "Catherine", "Delilah", "Elizabeth", "Fred", "George", "Hubert", "Ingred", "Julia",
    "Kyle", "Liam", "Maria", "Nancy", "Odin", "Patrick", "Quinn", "Riley", "Samantha", "Thomas", "Umer", "Victoria",
    "Witchita", "Xavior", "Yawn", "Zachary","Baby Jesus"};
    public static string GetRandomName()
    {
        return defaultNames[Random.Range(0, defaultNames.Length)];
    }

    //Chat filter
    #region Not for the faint of heart
    public static string[] wordsWeDontLike = new string[] { "tank", "t@nk", "artillery"};
    #endregion
    private static string[] filterAlternateMessages = new string[] { "I like bunny rabbits", "This winter is too long",
    "I'm such a noob", "Look it's a spider", "Mmm, that tasted purple"};
    public static string GetRandomMessage()
    {
        return filterAlternateMessages[Random.Range(0, filterAlternateMessages.Length)];
    }
}
