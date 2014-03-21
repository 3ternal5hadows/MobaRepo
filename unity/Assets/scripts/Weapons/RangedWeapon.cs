using UnityEngine;
using System.Collections;

public class RangedWeapon : Weapon
{
    public float normalCooldownTime;
    private Cooldown normalCooldown;

    public float chnceToNotConsumeCombo;
    public enum elemType { shadow, fire, ice, lightning, poison };
    elemType element;

    void Start()
    {
        base.Start();
    }

    void Update()
    {
        base.Update();
    }
}