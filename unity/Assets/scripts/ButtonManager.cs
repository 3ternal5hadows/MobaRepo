using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour
{

    // Use this for initialization
	public Texture2D ButtonTexture;
    float elapsedTime = 0;
	GUIStyle Style;
    void Start()
    {
        DataGod.currentGameState = DataGod.GameMode.Menu;
		GUIStyleState state = new GUIStyleState();
		state.background = ButtonTexture;
		Style = new GUIStyle();
		Style.border = new RectOffset(10,10,10,10);
		Style.padding = new RectOffset(10,10,10,10);
		Style.hover.background = ButtonTexture;
		Style.fontSize = 30;
		Style.fontStyle = FontStyle.Bold;
		Style.alignment = TextAnchor.MiddleCenter;
		Style.normal.background = ButtonTexture;
			
    }
    public float DelayTime = 20;
    bool loadingDemo = false;
    bool loadingWeapons = false;
    bool loadingGame = false;

    // Update is called once per frame
    void Update()
    {


        if (loadingGame || loadingDemo)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= DelayTime)
            {
                if (DataGod.isClient)
                {
                    Application.LoadLevel("WeaponSelect");
                }
                else
                {
                    Application.LoadLevel("level 1");
                }
            }
        }

    }

    void OnGUI()
    {
        if (!loadingDemo && !loadingGame)
        {	


            if (GUI.Button(new Rect(Screen.width/2 - 125, 200, 200, 75), "Server",Style))
            {
                DataGod.isClient = false;
                Camera.main.animation.Play();
                loadingGame = true;
                DataGod.currentGameState = DataGod.GameMode.NetWorkPlay;

            }
            if (GUI.Button(new Rect(Screen.width/2 -125, 350, 200, 75), "Client",Style))
            {
                DataGod.isClient = true;
                Camera.main.animation.Play();
                loadingGame = true;
                DataGod.currentGameState = DataGod.GameMode.NetWorkPlay;

            }
           
        }
        else GUI.enabled = false;

    }


}
