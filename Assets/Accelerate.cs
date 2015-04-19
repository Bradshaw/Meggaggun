using UnityEngine;
using System.Collections;

public class Accelerate : MonoBehaviour {

    Projectile prj;
    public float thrust;

	// Use this for initialization
	void Start () {
        prj = GetComponent<Projectile>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 vel = prj.velocity;
        vel+= prj.transform.forward * thrust * Time.deltaTime;
        prj.velocity = vel;
	}
}
