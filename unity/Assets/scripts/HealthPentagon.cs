using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthPentagon : MonoBehaviour {
    public List<Sprite> frames;
    private float alpha;

	// Use this for initialization
	void Start () {
        alpha = 0;
        transform.Rotate(new Vector3(1, 0, 0), 90);
	}
	
	// Update is called once per frame
	void Update () {
        if (alpha > 0)
        {
            alpha -= Time.deltaTime * 0.5f;
        }
        else
        {
            alpha = 0;
        }
        gameObject.GetComponent<SpriteRenderer>().color = Color.white * alpha;
	}

    public void SetPosition(Vector3 playerPosition)
    {
        transform.position = playerPosition + new Vector3(0, 2, 1);
    }

    public void Show(float health, float maxHealth)
    {
        alpha = 1.5f;
        int frame = (int)(health/maxHealth*(frames.Count-1));
        gameObject.GetComponent<SpriteRenderer>().sprite = frames[frame];
    }
}
