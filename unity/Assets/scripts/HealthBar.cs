using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthBar : MonoBehaviour {
    public List<Sprite> frames;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(10, 10, 220, 60), frames[0].texture);
    }
}
