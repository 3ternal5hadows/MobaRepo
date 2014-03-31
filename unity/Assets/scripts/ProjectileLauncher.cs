using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileLauncher : MonoBehaviour
{

    public GameObject projectile;

    public List<GameObject> weapons;

    private int currentWeaponEquipped = 0;

    public float ReloadSpeed = 1;
    public float ChargeRate = 0.1f;
    public float predShurikenForce = 0;
    public float predShurikenSpin = 0;

    GameObject chargingSpell;
    float scale = 0.1f;
    float reloadTime;
    bool MouseJustPressed;

    public bool down;

    void Start()
    {
        reloadTime = ReloadSpeed;
        MouseJustPressed = false;
        down = false;
    }

    public void Create()
    {
        if (reloadTime > ReloadSpeed)
        {
            chargingSpell = Network.Instantiate(projectile, this.transform.position, Quaternion.LookRotation(transform.forward), 0) as GameObject;
            chargingSpell.networkView.RPC("RPCSource", RPCMode.All, gameObject.GetComponent<DamageObject>().source);
            chargingSpell.transform.parent = this.transform;
            down = true;
        }
    }

    public void Launch()
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
            chargingSpell.rigidbody.AddForce(transform.parent.parent.parent.GetComponent<Rigidbody>().velocity + this.transform.forward * ((3000f * scale) + 300));
            scale = 0.1f;
            reloadTime = 0;
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
                reloadTime += Time.deltaTime;
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


            if (reloadTime >= ReloadSpeed)
            {

                if (Input.GetMouseButtonDown(0))
                {
                    chargingSpell = Instantiate(weapons[currentWeaponEquipped], this.transform.position, Quaternion.LookRotation(transform.forward)) as GameObject;
                    chargingSpell.GetComponent<DamageObject>().source = transform.parent.parent.gameObject.GetComponent<PlayerManager>().playerNumber;
                    if (chargingSpell.gameObject.tag == "projectile")
                    {
                        chargingSpell.transform.parent = this.transform;
                    }
                    if (chargingSpell.gameObject.tag == "predShuriken")
                    {
                        chargingSpell.gameObject.GetComponent<ShurikenScript>().addForce(predShurikenForce, transform.forward);
                        chargingSpell.gameObject.GetComponent<ShurikenScript>().addRotationalForce(predShurikenSpin, 1);
                        chargingSpell.gameObject.GetComponent<ShurikenScript>().player = this.transform.parent.parent.gameObject;
                    }

                    MouseJustPressed = true;

                }
                if (Input.GetMouseButton(0))
                {
                    scale += ChargeRate * Time.deltaTime;

                    //if(scale<1&&chargingSpell != null)
                    //	chargingSpell.GetComponent<Projectile>().SetScale(scale);
                    Debug.Log(scale);
                }

                if (Input.GetMouseButtonUp(0) && MouseJustPressed)
                {
                    MouseJustPressed = false;
                    if (chargingSpell != null)
                    {
                        if (chargingSpell.gameObject.tag == "projectile")
                        {
                            chargingSpell.transform.parent = null;
                            chargingSpell.AddComponent<Rigidbody>();
                            chargingSpell.GetComponent<SphereCollider>().enabled = true;
                            Debug.Log(scale);
                            if (scale > 1) scale = 1;
                            chargingSpell.rigidbody.useGravity = false;
                            chargingSpell.rigidbody.velocity = this.transform.parent.parent.rigidbody.velocity;
                            chargingSpell.rigidbody.AddForce(transform.parent.parent.GetComponent<Rigidbody>().velocity + this.transform.forward * ((3000f * scale) + 300));
                            scale = 0.1f;

                        }
                    }
                    //if(scale<=1)chargingSpell.GetComponent<Projectile>().SetScale(scale);

                    reloadTime = 0;
                }
            }
            reloadTime += Time.deltaTime;
        }
            #endregion

    }

}
