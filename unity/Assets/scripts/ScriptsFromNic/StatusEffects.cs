using UnityEngine;
using System.Collections;

//Got rid of some code redundancies,
//Added use of player's takeDamage method instead of modifying health directly
// - Greg :D
public class StatusEffectInfo
{
    public StatusEffects statusEffect;
    public float chanceToApply;

    public StatusEffectInfo(StatusEffects statusEffect, float chanceToApply)
    {
        this.statusEffect = statusEffect;
        this.chanceToApply = chanceToApply;
    }
}
public class StatusEffects  {
    public PlayerManager playerScript;
    public PlayerManager sourceScript;
	
	protected float elapsedTime;
    protected float duration;
	protected int count;

    public StatusEffects(float duration)
    {
        this.duration = duration;
        elapsedTime = 0;
    }

	// Update is called once per frame
	public virtual void Update () {
        elapsedTime += Time.deltaTime;
	}

    public bool Expired()
    {
        return (elapsedTime >= duration);
    }

    public virtual StatusEffects GetNewEffect()
    {
        return null;
    }
}
public class DamagingStatusEffect : StatusEffects
{
    protected int dps;
    protected int baseDamage; 

    public DamagingStatusEffect(float duration)
        : base(duration)
    {
    }

    public override StatusEffects GetNewEffect()
    {
        return new DamagingStatusEffect(duration);
    }
}
public class BurnEffect : DamagingStatusEffect {
    public BurnEffect(int baseDamage, float duration)
        : base(duration)
    {
        this.baseDamage = baseDamage;
        dps = (int)((baseDamage * 0.1f) / duration);
        count = 1;
    }
	
	// Update is called once per frame
	public override void Update () 
	{
        base.Update();
		if(elapsedTime >= count)
		{
			count++;
            playerScript.TakeDamage(dps, sourceScript.playerNumber, null);
		}
	}

    public override StatusEffects GetNewEffect()
    {
        return new BurnEffect(baseDamage, duration);
    }
}
public class FrostEffect : StatusEffects
{
    public float slowPercentage;
    public FrostEffect(float _duration, float slowPercentage)
        : base(_duration)
    {
        count = 1;
        this.slowPercentage = slowPercentage;
    }
	
	// Update is called once per frame
	public override void Update () 
	{
        base.Update();
	}

    public override StatusEffects GetNewEffect()
    {
        return new FrostEffect(duration, slowPercentage);
    }
}
public class BlindEffect : StatusEffects {
    public BlindEffect(float _duration)
        : base(_duration)
    {
        count = 1;
    }
	
	// Update is called once per frame
	public override void Update ()
	{
        base.Update();
		if(elapsedTime >= count)
			count++;
	}

    public override StatusEffects GetNewEffect()
    {
        return new BlindEffect(duration);
    }
}
public class ShockEffect : DamagingStatusEffect {
	float duration;
	public ShockEffect(float _duration) : base(_duration)
	{
		dps = 3;
		count = 1;
	}
	
	// Update is called once per frame
	public override void Update ()
	{
        base.Update();
		if(elapsedTime >= count)
		{
			count++;
            playerScript.TakeDamage(dps, sourceScript.playerNumber, null);
		}
	}
    public override StatusEffects GetNewEffect()
    {
        return new ShockEffect(duration);
    }
}
//public class PoisonEffect : DamagingStatusEffect {

//    int duration;
//    public PoisonEffect(float baseDamage, float duration) : base(duration)
//    {
//        dps = (int)(baseDamage * 0.05f) + 1;
//        count = 1;
//    }
	
//    //	IEnumerator killDuration()
//    //	{
//    //		playerObject.GetComponent<PlayerManager>().health -= dps;
//    //
//    //		yield return new WaitForSeconds(1);
//    //
//    //	}
//    // Update is called once per frame
//    public override void Update () 
//    {
//        base.Update();
//        if(elapsedTime >= count)
//        {
//            count++;
//            playerScript.TakeDamage(dps, null);
//        }
//    }
//}