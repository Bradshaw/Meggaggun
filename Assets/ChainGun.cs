using UnityEngine;
using System.Collections;

public class ChainGun : MonoBehaviour {

    public Projectile projectile;

    public float maxRate;
    public float spoolUpTime;
    public float spoolDownTime;

    public int ammo;

    Meggagun mg;

    float spool = 0;
    float cool = 1;
    bool firing = false;

	// Use this for initialization
	void Start () {
        mg = GetComponent<Meggagun>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        cool -= Time.deltaTime * spool * maxRate;
        if (cool <= 0)
        {
            if (firing && spool>0.5f && ammo>0)
            {
                float Spread = 7.0f;
                Projectile p = Instantiate<Projectile>(projectile);
                p.transform.rotation = mg.gunModel.transform.rotation;
                p.transform.Rotate(new Vector3(Random.Range(-Spread, Spread), Random.Range(-Spread, Spread), 0));
                p.transform.position = mg.gunModel.transform.position;
                p.velocity = p.transform.forward * 15;
                p.FiredBy = gameObject.transform.root.gameObject;
                ammo--;
            }
            cool += 1;
        }

        if (firing)
        {
            spool += Time.deltaTime * (1 / spoolUpTime);
        }
        else
            spool -= Time.deltaTime * (1 / spoolDownTime);
        spool = Mathf.Clamp(spool,0,1);
        firing = false;

	}

    void Fire()
    {
        if (ammo>0)
            firing = true;
    }

}
