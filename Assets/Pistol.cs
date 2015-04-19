using UnityEngine;
using System.Collections;

public class Pistol : MonoBehaviour {
    public Projectile projectile;

    public float rate;
    

    Meggagun mg;

    float cool = 0;
    bool firing = false;

    // Use this for initialization
    void Start()
    {
        mg = GetComponent<Meggagun>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (firing)
        {
            if (cool <= 0)
            {
                float Spread = 3.0f;
                Projectile p = Instantiate<Projectile>(projectile);
                p.transform.rotation = mg.gunModel.transform.rotation;
                p.transform.Rotate(new Vector3(Random.Range(-Spread, Spread), Random.Range(-Spread, Spread), 0));
                p.transform.position = mg.gunModel.transform.position;
                p.velocity = p.transform.forward * 20;
                p.FiredBy = gameObject.transform.root.gameObject;
                cool += 1;
            }
        }
        cool -= Time.deltaTime * rate;
        cool = Mathf.Clamp01(cool);
        firing = false;

    }

    void Fire()
    {
        firing = true;
    }
}
