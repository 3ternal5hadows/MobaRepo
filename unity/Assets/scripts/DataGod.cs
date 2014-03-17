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
}
