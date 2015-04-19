using UnityEngine;
using System.Collections;

public class Meggagun : MonoBehaviour {

    public Camera cam;
    public GameObject gunModel;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            this.gameObject.SendMessage("Fire");
        }
	}
}
