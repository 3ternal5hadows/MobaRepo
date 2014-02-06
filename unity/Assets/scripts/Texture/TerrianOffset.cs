using UnityEngine;
using System.Collections;

public class TerrianOffset : MonoBehaviour {
	public Terrain terrain;
	SplatPrototype[] splatPrototypes;
	void Start () {
	    splatPrototypes = this.terrain.terrainData.splatPrototypes;
	}
 
	void Update () {
	 
	    //splatPrototypes[3].tileOffset.x += 0.1f;
	    //terrain.terrainData.splatPrototypes[2].tileOffset.x+=0.1;
	
	    this.terrain.terrainData.splatPrototypes=splatPrototypes;
	}
}
