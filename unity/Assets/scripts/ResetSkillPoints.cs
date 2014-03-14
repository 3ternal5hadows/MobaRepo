using UnityEngine;
using System.Collections;

public class ResetSkillPoints : MonoBehaviour {

	// Use this for initialization
	GameObject[] nodes;
	bool nodesPopulated;
	void Start () {

		nodesPopulated = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!nodesPopulated)
		{
			nodes = GameObject.FindGameObjectsWithTag("Node");
			nodesPopulated = true;
		}
	}
	public void ResetNodes()
	{
		Application.LoadLevel("Skill Tree");

//		foreach(GameObject node in nodes)
//		{
//			node.GetComponent<Node>().num = 0;
//		}
	}


}
