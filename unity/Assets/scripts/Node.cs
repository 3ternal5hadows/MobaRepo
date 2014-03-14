using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {

	public int num = 0;
	public int MaxValue = 5;
	public int start = 1;
	public int none = 0;
	public GameObject node1;
	public GameObject node2;
	public GameObject node3;
	public GameObject dependNode1;
	public GameObject dependNode2;
	public GameObject dependNode3;

	public bool StartingPoint = false;
	bool unlocked = false;

	void Awake ()
	{

	}
	void OnLevelWasLoaded(int level)
	{
		if(level ==0)
		{
			node1.SetActive(false);
			node2.SetActive(false);
			node3.SetActive(false);
		}
	}

	// Use this for initialization
	void Start () {
		 
		if(StartingPoint)gameObject.SetActive(true);
		else gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {


		GetComponent<TextMesh>().text = num.ToString();

		if (unlocked == false && num >= MaxValue)
		{
			unlocked = true;
			if (node1 != null)
			{
				node1.SetActive(true);
			}
			
			if (node2 != null)
			{
				node2.SetActive(true);
			}

			if (node3 != null)
			{
				node3.SetActive(true);
			}			
		}	
	}
}
