using UnityEngine;
using System.Collections;

//Got rid of some code redundancies,
//Added use of player's takeDamage method instead of modifying health directly
// - Greg :D
public class StatusEffects  {
	//public GameObject playerObject;
    public PlayerManager playerScript;
	
	protected float elapsedTime;
    private float duration;
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
}
public class DamagingStatusEffect : StatusEffects
{
    public int dps;

    public DamagingStatusEffect(float duration)
        : base(duration)
    {
    }
}
public class BurnEffect : DamagingStatusEffect {
	int duration ;
    public BurnEffect(float baseDamage, float duration)
        : base(duration)
    {
        dps = (int)((baseDamage * 0.1f) / duration);
        count = 1;
    }
	
	// Update is called once per frame
	public override void Update () 
	{
		if(elapsedTime >= count)
		{
			count++;
			//playerObject.GetComponent<PlayerManager>().health -= dps;
            playerScript.TakeDamage(dps, null);
		}
	}
}
public class FrostEffect : StatusEffects
{
	float duration;
    public FrostEffect(float _duration)
        : base(_duration)
    {
        count = 1;
    }
	
	// Update is called once per frame
	public override void Update () 
	{
	}
}
public class BlindEffect : StatusEffects {
	float duration;
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
		if(elapsedTime >= count)
		{
			count++;
			playerScript.TakeDamage(dps, null);
		}
	}
}
public class PoisonEffect : DamagingStatusEffect {

	int duration;
	public PoisonEffect(float baseDamage, float duration) : base(duration)
	{
		dps = (int)(baseDamage * 0.05f) + 1;
		count = 1;
	}
	
	//	IEnumerator killDuration()
	//	{
	//		playerObject.GetComponent<PlayerManager>().health -= dps;
	//
	//		yield return new WaitForSeconds(1);
	//
	//	}
	// Update is called once per frame
	public override void Update () 
	{
		if(elapsedTime >= count)
		{
			count++;
            playerScript.TakeDamage(dps, null);
		}
	}
}