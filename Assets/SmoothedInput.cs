using UnityEngine;
using System.Collections;

public class SmoothedInput : MonoBehaviour {

    public static float smoothedX = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        smoothedX = (smoothedX + Input.GetAxis("Mouse X")) / 2;
	}
}
