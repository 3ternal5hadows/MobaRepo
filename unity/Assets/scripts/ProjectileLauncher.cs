using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileLauncher : MonoBehaviour
{

    public GameObject projectile;

    public List<GameObject> weapons;

    private int currentWeaponEquipped = 0;

    public float ChargeRate = 0.1f;
    public float predShurikenForce = 0;
    public float predShurikenSpin = 0;

    GameObject chargingSpell;
    float scale = 0.1f;
    bool MouseJustPressed;

    public bool down;

    void Start()
    {
        MouseJustPressed = false;
        down = false;
    }

    public void Create()
    {
        chargingSpell = Network.Instantiate(projectile, this.transform.position, Quaternion.LookRotation(transform.forward), 0) as GameObject;
        chargingSpell.networkView.RPC("RPCSource", RPCMode.All, gameObject.GetComponent<DamageObject>().source);
        chargingSpell.transform.parent = this.transform;
        down = true;
    }

    public void PowerAttack()
    {
        chargingSpell = Network.Instantiate(projectile, this.transform.position, Quaternion.LookRotation(transform.forward), 0) as GameObject;
        chargingSpell.networkView.RPC("RPCSource", RPCMode.All, gameObject.GetComponent<DamageObject>().source);
        chargingSpell.transform.parent = this.transform;
        Launch();
        chargingSpell = Network.Instantiate(projectile, this.transform.position, Quaternion.LookRotation(-transform.forward), 0) as GameObject;
        chargingSpell.networkView.RPC("RPCSource", RPCMode.All, gameObject.GetComponent<DamageObject>().source);
        chargingSpell.transform.parent = this.transform;
        Launch(-transform.forward);
        chargingSpell = Network.Instantiate(projectile, this.transform.position, Quaternion.LookRotation(new Vector3(transform.forward.z, 0, -transform.forward.x)), 0) as GameObject;
        chargingSpell.networkView.RPC("RPCSource", RPCMode.All, gameObject.GetComponent<DamageObject>().source);
        chargingSpell.transform.parent = this.transform;
        Launch(new Vector3(transform.forward.z, 0, -transform.forward.x));
        chargingSpell = Network.Instantiate(projectile, this.transform.position, Quaternion.LookRotation(new Vector3(-transform.forward.z, 0, transform.forward.x)), 0) as GameObject;
        chargingSpell.networkView.RPC("RPCSource", RPCMode.All, gameObject.GetComponent<DamageObject>().source);
        chargingSpell.transform.parent = this.transform;
        Launch(new Vector3(-transform.forward.z, 0, transform.forward.x));
    }

    public void Launch()
    {
        Launch(this.transform.forward);
    }

    public void Launch(Vector3 direction)
    {
        if (chargingSpell != null)
        {
            chargingSpell.transform.parent = null;
            chargingSpell.AddComponent<Rigidbody>();
            chargingSpell.GetComponent<SphereCollider>().enabled = true;
            //if(scale<=1)chargingSpell.GetComponent<Projectile>().SetScale(scale);
            Debug.Log(scale);
            if (scale > 1) scale = 1;
            chargingSpell.rigidbody.useGravity = false;
            chargingSpell.rigidbody.velocity = this.transform.parent.parent.parent.rigidbody.velocity;
            chargingSpell.rigidbody.AddForce(transform.parent.parent.parent.GetComponent<Rigidbody>().velocity + direction * ((3000f * scale) + 300));
            scale = 0.1f;
            chargingSpell = null;
            down = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        #region networking
        if (DataGod.currentGameState == DataGod.GameMode.NetWorkPlay)
        {
            if (networkView.isMine)
            {
                if (down)
                {
                    scale += ChargeRate * Time.deltaTime;
                }
            }
        #endregion
            #region DemoMode
        }
        else if (DataGod.currentGameState == DataGod.GameMode.Demo)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentWeaponEquipped = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                currentWeaponEquipped = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                currentWeaponEquipped = 2;
            }
        }
            #endregion

    }

}
