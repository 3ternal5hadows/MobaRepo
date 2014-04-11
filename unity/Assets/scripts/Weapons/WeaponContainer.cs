using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponContainer : MonoBehaviour {
    public GameObject sword;
    public GameObject projectileLauncher;
	public GameObject hammer;
    private List<GameObject> allWeapons;
    public string name;

	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
	}

    public Weapon InstantiateWeapon(int weaponNum, int ID)
    {
        allWeapons = new List<GameObject>();
        allWeapons.Add(sword);
        allWeapons.Add(projectileLauncher);
		//allWeapons.Add (hammer);

        if (weaponNum >= allWeapons.Count)
        {
            weaponNum = allWeapons.Count - 1;
        }

        GameObject weapon = (GameObject)Network.Instantiate(allWeapons[weaponNum], transform.position, transform.rotation, 0);
        Weapon weaponScript = weapon.GetComponent<Weapon>();
        weaponScript.ID = ID;
        weaponScript.Equipped = (ID != WeaponData.UNEQUIPPEDWEAPON);
        weaponScript.networkView.RPC("SetParent", RPCMode.AllBuffered, transform.parent.networkView.viewID);
        return weaponScript;
    }
}
