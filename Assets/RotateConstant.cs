using UnityEngine;
using System.Collections;

public class RotateConstant : MonoBehaviour {

    public Vector3 rotation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 rot = transform.localRotation.eulerAngles;
        rot += rotation * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(rot);
	}
}
