using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponContainer : MonoBehaviour {
    public GameObject sword;
    public List<GameObject> allWeapons;
    public string name;

	// Use this for initialization
	void Start () {
        allWeapons = new List<GameObject>();
        allWeapons.Add(sword);
	}
	// Update is called once per frame
	void Update () {
	}

    public Weapon InstantiateWeapon(int ID)
    {
        GameObject weapon = (GameObject)Network.Instantiate(sword, transform.position, transform.rotation, 0);
        Weapon weaponScript = weapon.GetComponent<Weapon>();
        weaponScript.ID = ID;
        weaponScript.Equipped = (ID != WeaponData.UNEQUIPPEDWEAPON);
        weaponScript.networkView.RPC("SetParent", RPCMode.AllBuffered, transform.parent.networkView.viewID);
        return weaponScript;
    }
}
