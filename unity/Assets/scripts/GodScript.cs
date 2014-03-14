using UnityEngine;
using System.Collections;

public class GodScript : MonoBehaviour {
	Ray wes;
	RaycastHit hit;
	public int maxTalent = 20;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			wes = Camera.main.ScreenPointToRay(Input.mousePosition);
			Debug.DrawRay (wes.origin, wes.direction * 10, Color.magenta);
			
			if (Physics.Raycast (wes, out hit, 10))
			{
				if (hit.transform.gameObject.tag == "Node")
				{
					if (hit.transform.gameObject.GetComponent<Node>().num <= 4)
					{
						hit.transform.gameObject.GetComponent<Node>().num ++;
						maxTalent --;
					}
				}
				if(hit.transform.gameObject.tag == "LevelLoader")
				{
					hit.transform.gameObject.GetComponent<LoadLevelOnClick>().LoadLevel();
				}

			}
		}
		if (Input.GetMouseButtonDown(1))
		{
			wes = Camera.main.ScreenPointToRay(Input.mousePosition);
			Debug.DrawRay (wes.origin, wes.direction * 10, Color.magenta);
			
			if (Physics.Raycast (wes, out hit, 10))
			{
				if (hit.transform.gameObject.tag == "Node")
				{
					if (hit.transform.gameObject.GetComponent<Node>().num >= 1)
					{
						hit.transform.gameObject.GetComponent<Node>().num --;
						maxTalent ++;
					}
				}
			}
		}
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel(0);
		}
	}
}
