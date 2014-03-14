using UnityEngine;
using System.Collections;

public class AttackAnimation : MonoBehaviour {

	public float Attack1CritWindow;
	public float Attack2CritWindow;
	public float Attack3CritWindow;

	public float WindowSize;

	float timeElapsedSinceLastAttack=0;
	float timeElapsed=0;

	int counter = 0;
	int combo =0;
	bool animationPlay;
	float animationLength;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		
		if (counter != 0||combo>0) {
			timeElapsedSinceLastAttack+= Time.deltaTime/2f;
		}

		if (Input.GetKeyDown (KeyCode.Mouse1) && counter == 0) {

			animationLength = animation["newSword1"].length;

			if(combo>0)
			{
				if(timeElapsedSinceLastAttack > (animationLength * Attack1CritWindow)-WindowSize 
				   && timeElapsedSinceLastAttack < (animationLength * Attack1CritWindow)+WindowSize)
				{
					combo++;
					Debug.Log("AttackCrit Combo "+combo);
					animation.PlayQueued("newSword1");
					counter++;

				}else 
				{
					combo =0;
					counter=0;
				}


				timeElapsedSinceLastAttack=0;
			}else 
			{
				animation.PlayQueued("newSword1");
				counter++;
				timeElapsedSinceLastAttack=0;
			}
			
		}
		else if (Input.GetKeyDown (KeyCode.Mouse1) && counter == 1) {

			animationLength = animation["newSword2"].length;

			if(timeElapsedSinceLastAttack > (animationLength * Attack2CritWindow)-WindowSize 
			   && timeElapsedSinceLastAttack < (animationLength * Attack2CritWindow)+WindowSize)
			{
				combo++;
				Debug.Log("AttackCrit Combo "+combo);

				animation.PlayQueued("newSword2");
				timeElapsedSinceLastAttack=0;
				counter++;
			}else
			{
				combo =0;
				counter =0;
			}
		

		}
		else if (Input.GetKeyDown (KeyCode.Mouse1) && counter == 2) {

			animationLength = animation["newSword3"].length;

			if(timeElapsedSinceLastAttack > (animationLength * Attack3CritWindow)-WindowSize 
			   && timeElapsedSinceLastAttack < (animationLength * Attack3CritWindow)+WindowSize)
			{
				animation.PlayQueued("newSword3");
				combo++;
				Debug.Log("AttackCrit Combo "+combo);

				timeElapsedSinceLastAttack=0;
				counter = 0;
			}else
			{
				combo =0;
				counter =0;
			}
		


		}
	
	}
}
