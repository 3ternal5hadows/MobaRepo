using UnityEngine;
using System.Collections;

public class TextureRotation : MonoBehaviour {
	
	
	public Vector2 scrollSpeed = Vector2.zero;	
	Vector2 offset;
	
	
	void Start () {
	
	}
	

	void Update () {
		if(renderer != null)
		{
		offset = new Vector2(Time.time * scrollSpeed.x,Time.time * scrollSpeed.y);
        renderer.material.SetTextureOffset("_MainTex", offset);
		renderer.material.SetTextureOffset("_BumpMap", offset);
		renderer.material.SetTextureOffset("_Detail", offset);
		}
	}
}
