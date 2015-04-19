using UnityEngine;
using System.Collections;

public class DoomGuyMovement : MonoBehaviour {

    public float runSpeed;
    public float rotationPower;

    Rigidbody2D _rig;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _rig = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        _rig.MoveRotation(_rig.rotation + SmoothedInput.smoothedX*rotationPower);
        _rig.AddForce((new Vector2(Input.GetAxis("Vertical") * runSpeed, Input.GetAxis("Horizontal") * runSpeed)).rotate(_rig.rotation), ForceMode2D.Force);
	}
}
