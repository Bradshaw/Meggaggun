using UnityEngine;
using System.Collections;

public class GunSway : MonoBehaviour {

    public float speed;
    public float rotationPower;
    public float movePower;
    public float vBobAmp;
    public float hBobAmp;
    [Range(0,Mathf.PI*2)]
    public float bobPhase;
    public float bobFreq;
    public float freqMult;

    public float maxDist;

    Vector3 vel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.localPosition;
        float move = Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical"));
        move = Mathf.Clamp01(move);
        pos.y += SmoothedInput.smoothedX * rotationPower + Input.GetAxis("Horizontal") * movePower + hBobAmp * move * Mathf.Sin(Time.time * bobFreq * Mathf.PI);
        pos.x += Input.GetAxis("Vertical") * movePower;
        pos.z += vBobAmp * move * Mathf.Sin((Time.time+bobPhase) * bobFreq * Mathf.PI * freqMult);
        pos = pos.normalized * Mathf.Clamp(pos.magnitude, 0, maxDist);
        transform.localPosition = pos;
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, Vector3.zero, ref vel, speed);
	}
}
