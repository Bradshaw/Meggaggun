using UnityEngine;
using System.Collections;

public class Dude : MonoBehaviour {

    public Camera cam;
    public GameObject camStrap;

    int hitpoints = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void Impact(ImpactData id)
    {
        hitpoints -= id.projectile.damage;
        if (hitpoints <= 0)
        {
            GameObject go = Instantiate(camStrap);
            Rigidbody rig = go.GetComponent<Rigidbody>();
            rig.AddTorque(Random.rotation.eulerAngles*5,ForceMode.Impulse);
            go.transform.position = cam.transform.position;
            go.transform.rotation = Random.rotation;
            cam.transform.parent = go.transform;
            Destroy(this.gameObject);
        }
    }

}
