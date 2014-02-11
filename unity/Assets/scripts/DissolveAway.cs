 using UnityEngine;
using System.Collections;

public class DissolveAway : MonoBehaviour {

	// Use this for initialization


	public bool dissolving;
	public bool dissolved;

	public bool undissolved;
	public float sliceAmount=0;
	public float dissolveRate=1;

	void Start () {

		dissolving = false;
	}
	
	 //Update is called once per frame
	void Update () {

		if(dissolving)
		{

			sliceAmount+= dissolveRate*Time.deltaTime;
			if(sliceAmount>1)sliceAmount = 1;
			renderer.material.SetFloat("_SliceAmount",sliceAmount);
		}
		else if(!dissolving && sliceAmount > 0 && !undissolved)
		{
			sliceAmount -= dissolveRate*Time.deltaTime;
			renderer.material.SetFloat("_SliceAmount",sliceAmount);
		}
		if(sliceAmount<0)
		{
			sliceAmount = 0;
		}

		dissolving = false;
	}
	public void startDissolving()
	{
		dissolving = true;
	}

}
