using UnityEngine;
using System.Collections;

public class VertexPaintingTextureOffsetting : MonoBehaviour {
	public bool Splat1;
	public Vector2 scrollSpeed1 = Vector2.zero;
	Vector2 offSet1;
	public bool Splat2;
	public Vector2 scrollSpeed2 = Vector2.zero;
	Vector2 offSet2;
	public bool Splat3;
	public Vector2 scrollSpeed3 = Vector2.zero;
	Vector2 offSet3;
	public bool Splat4;
	public Vector2 scrollSpeed4 = Vector2.zero;
	Vector2 offSet4;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Splat1)
		{
			offSet1 = new Vector2 (Time.time * scrollSpeed1.x, Time.time * scrollSpeed1.y);
			renderer.material.SetTextureOffset ("_Splat1", offSet1);
			renderer.material.SetTextureOffset ("_Splat1Bump", offSet1);
		}
		if(Splat2)
		{
			offSet2 = new Vector2 (Time.time * scrollSpeed2.x, Time.time * scrollSpeed2.y);
			renderer.material.SetTextureOffset ("_Splat2", offSet2);
			renderer.material.SetTextureOffset ("_Splat1Bump", offSet2);
		}
		if(Splat3)
		{
			offSet3 = new Vector2 (Time.time * scrollSpeed3.x, Time.time * scrollSpeed3.y);
			renderer.material.SetTextureOffset ("_Splat3", offSet3);
			renderer.material.SetTextureOffset ("_Splat1Bump", offSet3);
		}
		if(Splat4)
		{
			offSet4 = new Vector2 (Time.time * scrollSpeed4.x, Time.time * scrollSpeed4.y);
			renderer.material.SetTextureOffset ("_Splat4", offSet4);
			renderer.material.SetTextureOffset ("_Splat1Bump", offSet4);
		}
	
	}
}
