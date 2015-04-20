using UnityEngine;
using System.Collections;

public class Dude : MonoBehaviour {

    public AudioSource src;

    public Camera cam;
    public GameObject camStrap;

    public int hitpoints = 100;

	// Use this for initialization
	void Start () {
        fillRandom();
	}
	
    public void fillRandom()
    {
        ammoType at = (ammoType)Random.Range(0, 4);
        switch (at)
        {
            case ammoType.laser:
                GetComponent<Laser>().ammo = 1000;
                break;
            case ammoType.rocket:
                GetComponent<Rocket>().ammo += 1000;
                break;
            case ammoType.nade:
                GetComponent<GrenadeLauncher>().ammo += 1000;
                break;
            case ammoType.chain:
                GetComponent<ChainGun>().ammo += 1000;
                break;
        }
    }

	// Update is called once per frame
	void Update () {
	    
	}

    void Impact(ImpactData id)
    {
        hitpoints -= id.projectile.damage;
        AudioSource.PlayClipAtPoint(src.clip, this.transform.position);
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
