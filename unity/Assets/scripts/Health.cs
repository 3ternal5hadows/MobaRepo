using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {


    public Texture2D h00;
    public Texture2D h10;
    public Texture2D h20;
    public Texture2D h30;
    public Texture2D h40;
    public Texture2D h50;
    public Texture2D h60;
    public Texture2D h70;
    public Texture2D h80;
    
    static int HP = 80;


	void Start () {
        InvokeRepeating("Subtract", 2f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
        GameObject go = GameObject.Find("gHealth");
        if (HP > 70)
        {
            go.guiTexture.texture = h80;
        }
        else if (HP > 60)
        {
            go.guiTexture.texture = h70;
        }
        else if (HP > 50)
        {
            go.guiTexture.texture = h60;
        }
        else if (HP > 40)
        {
            go.guiTexture.texture = h50;
        }
        else if (HP > 30)
        {
            go.guiTexture.texture = h40;
        }
        else if (HP > 20)
        {
            go.guiTexture.texture = h30;
        }
        else if (HP > 10)
        {
            go.guiTexture.texture = h20;
        }
        else if (HP > 0)
        {
            go.guiTexture.texture = h10;
        }
        else { go.guiTexture.texture = h00; }
	
	}
    void Subtract()
    {
        HP -= 1;
        Debug.Log(HP);
    }
}
