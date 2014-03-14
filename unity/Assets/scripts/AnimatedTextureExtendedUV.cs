using UnityEngine;
using System.Collections;
//copied from http://wiki.unity3d.com/index.php?title=Animating_Tiled_texture_-_Extended
public class AnimatedTextureExtendedUV : MonoBehaviour
{
	
	//vars for the whole sheet
	public int colCount =  10;
	public int rowCount =  30;
	
	//vars for animation
	int rowNumber; //Zero Indexed
	int colNumber; //Zero Indexed
	public int totalCells = 300;
	public int  fps     = 30;	
	//Maybe this should be a private var
	private Vector2 offset;
	public float switchTime=1;
	int totalRows;
	float elapsedTime;
	void Start()
	{
		rowNumber= 0;
		colNumber =0;
	}

	//Update
	void Update () 
	{
//		elapsedTime += Time.deltaTime;
//		if(elapsedTime >= switchTime)
//		{
//			elapsedTime = 0;
//			rowNumber++;
//			Debug.Log("rowNumber " + rowNumber);
//			if(rowNumber>=rowCount)
//			{
//				rowNumber =0;
//			}
//		}
		
		// Calculate index
		int index  = (int)(Time.time * fps);
		// Repeat when exhausting all cells
		index = index % totalCells;
		
		// Size of every cell
		float sizeX = 1.0f / colCount;
		float sizeY = 1.0f / rowCount;
		Vector2 size =  new Vector2(sizeX,sizeY);
		
		// split into horizontal and vertical index
		var uIndex = index % colCount;
		var vIndex = index / colCount;
		
		// build offset
		// v coordinate is the bottom of the image in opengl so we need to invert.
		
		float offsetX = (uIndex+colNumber) * size.x;
		float offsetY = (1.0f - size.y) - (vIndex + rowNumber) * size.y;
		Vector2 offset = new Vector2(offsetX,offsetY);
		
		renderer.material.SetTextureOffset ("_MainTex", offset);
		renderer.material.SetTextureScale  ("_MainTex", size); 
		renderer.material.SetTextureOffset ("_BumpMap", offset);
		renderer.material.SetTextureScale  ("_BumpMap", size); 
		renderer.material.SetTextureOffset ("_ParrallaxMap", offset);
		renderer.material.SetTextureScale  ("_ParrallaxMap", size); 
		renderer.material.SetTextureOffset ("_Detail", offset);
		renderer.material.SetTextureScale  ("_Detail", size); 
	}
	
	//SetSpriteAnimation
	void SetSpriteAnimation(int _colCount ,int _rowCount ,int _rowNumber ,int _colNumber,int totalCells,int fps ){


	}
}
