using UnityEngine;
using System.Collections;

public class Spell : Weapon
{
    public float normalCooldownTime;
    private Cooldown normalCooldown;

    public float comboTmrLngth;

    public enum elemType { shadow, fire, ice, lightning };
    elemType primary;
    elemType secondary;

    public bool didItCrit()
    {
        return false;//procChnce >= Random.Range(0.0f, 100.0f);
    }
}