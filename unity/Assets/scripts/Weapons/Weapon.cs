using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    //skillTree Effects on all weapons

    public float atkSpd;
    public float pwrAtkActivateSpd;
    public float pwrtAtkCoolDown;
    public float pwrAtkDmg;
    public float nrmAtkDmg;
    public float critDmgBns;
    public float specEffectivePrcnt;
    public float procChnce;

    public StatusEffectInfo element;

    protected void Start()
    {

    }

    protected void Update()
    {
    }

    public StatusEffects GetStatusEffect()
    {
        if (Random.Range(0, 100) / 100.0f <= element.chanceToApply)
        {
            return element.statusEffect.GetNewEffect();
        }
        else
        {
            return null;
        }
    }
}