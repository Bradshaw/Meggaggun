using UnityEngine;
using System.Collections;

public class Meggagun : MonoBehaviour {

    public ParticleSystem particles;
    public Camera cam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray r = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit rh;
            if (Physics.Raycast(r, out rh))
            {
                particles.transform.position = rh.point+rh.normal*0.1f;
                particles.transform.rotation = Quaternion.LookRotation(rh.normal);
                particles.Emit(Random.Range(5,10));
            }
        }
	}
}
