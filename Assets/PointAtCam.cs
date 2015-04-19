using UnityEngine;
using System.Collections;

public class PointAtCam : MonoBehaviour {

    Camera _cam;
    Camera cam
    {   
        get{
            if (_cam == null)
                _cam = Camera.main;
            return _cam;
        }
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 targ = cam.transform.position;
        targ.z = transform.position.z;
        transform.LookAt(targ, Vector3.forward);
	}
}
