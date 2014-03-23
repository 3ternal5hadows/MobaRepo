using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {
    //Team stuff
    public int numTeams;
    public int[] teamScore;


	// Use this for initialization
    void Start()
    {
        numTeams = GameObject.FindGameObjectsWithTag("SpawnPoint").Length;
        teamScore = new int[numTeams];
        for (int i = 0; i < teamScore.Length; i++)
        {
            teamScore[i] = 0;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    [RPC]
    public void RPCAddKill(int team)
    {
        teamScore[team]++;
    }
}
