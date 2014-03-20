using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HUD : MonoBehaviour {
    public List<Sprite> powerCooldownFrames;
    public List<Sprite> normalCooldownFrames;
    public List<Sprite> weaponIcons;
    public List<Sprite> frames;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnGUI()
    {
        if (networkView != null)
        {
            if (networkView.isMine)
            {
                int frame = (int)(gameObject.GetComponent<PlayerManager>().GetHealthPercentage() * (frames.Count - 1));
                GUI.DrawTexture(new Rect(10, 10, 220, 60), frames[frame].texture);
                Debug.Log(frame);
            }
        }
    }
}
