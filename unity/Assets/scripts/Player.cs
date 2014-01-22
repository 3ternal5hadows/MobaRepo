using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
        time = 0;
        doorIsOpen = false;
	}
	float time;
    bool doorIsOpen;
    GameObject door;
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        
        if (Physics.Raycast(ray, out hit, 2) && !doorIsOpen)
        {
            if (hit.collider.gameObject.tag == "Door")
            {
                door = hit.collider.gameObject;
                Debug.Log("Door Open");
                hit.collider.gameObject.animation.Play("Door_Open");
                doorIsOpen = true;
                time = 0;
            }            
        }
        if (time > 5 && doorIsOpen)
        {
            Debug.Log("Door Close");
            //GameObject.Find("Door").animation.Play("Door_Close");
            door.animation.Play("Door_Close");
            doorIsOpen = false;
        }
        else { time += Time.deltaTime; }
	}
}
