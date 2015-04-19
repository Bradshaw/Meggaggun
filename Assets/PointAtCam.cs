using UnityEngine;
using System.Collections;

public class PointAtCam : MonoBehaviour {

    public Camera cam;

	// Use this for initialization
	void Start () {
        if (cam == null)
            cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targ = cam.transform.position;
        targ.z = transform.position.z;
        transform.LookAt(targ, Vector3.forward);
	}
}
