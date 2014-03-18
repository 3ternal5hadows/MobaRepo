using UnityEngine;
using System.Collections;

public class SkillTreeButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool MouseHit(Vector2 mousePosition)
    {
        //122 x 31
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        return (mousePosition.x > position.x - 1.22f & mousePosition.x < position.x + 1.22f &
            mousePosition.y > position.y - 0.31f & mousePosition.y < position.y + 0.31f);
    }
}
