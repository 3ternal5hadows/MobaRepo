using UnityEngine;
using System.Collections;

public class GrapHook : MonoBehaviour {
    
    Vector3 RightHookLocation;
    Vector3 LeftHookLocation;
    float hookLength;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButtonDown(0))//Left Click
        {
            DetectLeftHook();

        }
        if (Input.GetMouseButtonDown(1))
        {
            DetectRightHook();
        }
        
	}
    void DetectLeftHook()
    {
        //Vector3 CameraAngle = 
    }
    void DetectRightHook()
    {
    }
}
