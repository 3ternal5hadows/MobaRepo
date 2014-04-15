using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponContainer : MonoBehaviour {
    public GameObject sword;
	public GameObject axe;
	public GameObject dagger;
	public GameObject hammer;
	public GameObject meteor;
    public GameObject projectileLauncher;
	public GameObject line;
	public GameObject pred;


    private List<GameObject> allWeapons;
    public string name;

	// Use this for initialization
	void Awake () {
		allWeapons = new List<GameObject>();
		allWeapons.Add(sword);
		allWeapons.Add(dagger);
		allWeapons.Add(hammer);
		allWeapons.Add(meteor);
		allWeapons.Add(projectileLauncher);
		allWeapons.Add(line);
		allWeapons.Add(pred);
	}
	// Update is called once per frame
	void Update () {
	}

    public Weapon InstantiateWeapon(int weaponNum, int ID)
    {   
		if(allWeapons == null)
		{
			Debug.Log(gameObject.name);
			Debug.Log(transform.parent.gameObject.name);
		}
		if (weaponNum >= allWeapons.Count)
        {
            weaponNum = allWeapons.Count - 1;
        }

        GameObject weapon = (GameObject)Network.Instantiate(allWeapons[weaponNum], transform.position, transform.rotation, 0);
        Weapon weaponScript = weapon.GetComponent<Weapon>();
        weaponScript.ID = ID;

        weaponScript.networkView.RPC("SetParent", RPCMode.AllBuffered, transform.parent.networkView.viewID);
        return weaponScript;
    }
}
