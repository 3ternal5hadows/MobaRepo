using UnityEngine;
using System.Collections;

public class MeleeWeapon : Weapon
{
    public float critWindowRnge;
    //public enum elemType { shadow, fire, ice, lightning };
    //elemType element;

    void Start()
    {
        base.Start();
        element = new StatusEffectInfo(new BurnEffect(100, 5), 0.1f);
    }

    void Update()
    {
        base.Update();
    }
}