using UnityEngine;
using System.Collections;

public class DataGod {
    public static bool isClient;
	public static bool networkIsMine = false;
	public enum GameMode{ Menu, Demo, NetWorkPlay};	
	public static GameMode currentGameState;
}
