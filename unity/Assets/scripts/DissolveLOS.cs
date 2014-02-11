using UnityEngine;
using System.Collections;

public class DissolveLOS : MonoBehaviour {

	// Use this for initialization
	public Material dissolve;
	private Material startingMat;
	public GameObject player;
	RaycastHit hitInfo;
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine(this.transform.position, player.transform.position, Color.blue);
		if(Physics.Linecast(this.transform.position, player.transform.position, out hitInfo , 1))
		{
			if(hitInfo.transform.gameObject.tag == "Dissolvable")			
			{
				hitInfo.transform.gameObject.GetComponent<DissolveAway>().startDissolving();
				Debug.DrawLine(this.transform.position, player.transform.position, Color.yellow);
			}
		}else Debug.DrawLine(this.transform.position, player.transform.position, Color.blue);
	}
}
