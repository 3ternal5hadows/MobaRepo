using UnityEngine;
using System.Collections;

public class StatusEffects  {
	public GameObject playerObject;
	public float dps;
	
	public float elapsedTime;
	public int count;
	
	// Update is called once per frame
	public virtual void Update () {
	
	}
}
public class BurnEffect : StatusEffects {
	int duration ;
	public BurnEffect (float baseDamage) 
	{
		elapsedTime = 0;
		duration = 5;
		dps = (baseDamage * 0.1f) / duration;
		count = 1;
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		elapsedTime += Time.deltaTime;
		if(elapsedTime >= count)
		{
			count++;
			playerObject.GetComponent<PlayerManager>().health -= dps;
		}
		if(elapsedTime >= duration)
			playerObject.GetComponent<PlayerManager>().statusEffectsOnPlayer.Remove(this);
	}
}
public class FrostEffect : StatusEffects
{
	float duration;
	public FrostEffect (float _duration) 
	{
		elapsedTime = 0;
		duration = _duration;
		dps = 0;
		count = 1;
		playerObject.GetComponent<PlayerManager>().Freeze(duration);
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		elapsedTime += Time.deltaTime;
		if(elapsedTime >= count)
			count++;
		if(elapsedTime >= duration)
			playerObject.GetComponent<PlayerManager>().statusEffectsOnPlayer.Remove(this);
	}
}
public class BlindEffect : StatusEffects {
	float duration;
	public BlindEffect(float _duration)
	{
		elapsedTime = 0;
		duration = _duration;
		count = 1;
		playerObject.GetComponent<PlayerManager>().Blind(duration);
	}
	
	// Update is called once per frame
	public override void Update ()
	{
		elapsedTime += Time.deltaTime;
		if(elapsedTime >= count)
			count++;
		if(elapsedTime >= duration)
			playerObject.GetComponent<PlayerManager>().statusEffectsOnPlayer.Remove(this);
	}
}
public class ShockEffect : StatusEffects {
	float duration;
	public ShockEffect(float _duration) 
	{
		elapsedTime = 0;
		duration = _duration;
		dps = 3;
		count = 1;
		playerObject.GetComponent<PlayerManager>().Shock(duration);
	}
	
	// Update is called once per frame
	public override void Update ()
	{
		elapsedTime += Time.deltaTime;
		if(elapsedTime >= count)
		{
			count++;
			playerObject.GetComponent<PlayerManager>().health -= dps;
		}
		if(elapsedTime >= duration)
			playerObject.GetComponent<PlayerManager>().statusEffectsOnPlayer.Remove(this);
	}
}
public class PoisonEffect : StatusEffects {

	int duration;
	public PoisonEffect(float baseDamage)
	{
		elapsedTime = 0;
		dps = (baseDamage * 0.05f) + 1;
		duration = 5;
		count = 1;
		playerObject.GetComponent<PlayerManager>().Poison(baseDamage);
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
		elapsedTime += Time.deltaTime;
		if(elapsedTime >= count)
		{
			count++;
			playerObject.GetComponent<PlayerManager>().health -= dps;
		}
		if(elapsedTime >= duration)
			playerObject.GetComponent<PlayerManager>().statusEffectsOnPlayer.Remove(this);
		
	}
}