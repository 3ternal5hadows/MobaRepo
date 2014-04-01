using UnityEngine;
using System.Collections;

public class WeaponData {
    public static int currentTree = 0;
    //Refer to NodeIDReference.png to see which nodes these value correspond with
    //Located in Assets/Scripts/Skill Tree/
    public static int[,] treeData = new int[3, 10] { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
    public static int[] weapons = new int[3] { 0, 0, 0 };

    public const int LEFTHANDWEAPON = 0;
    public const int RIGHTHANDWEAPON = 1;
    public const int UNEQUIPPEDWEAPON = 2;

	public static void resetWeaponDisplay()
	{
		treeData = new int[3,10]{ { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
								{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
								{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };	

		weapons = new int[3] {0,0,0};

	}
}