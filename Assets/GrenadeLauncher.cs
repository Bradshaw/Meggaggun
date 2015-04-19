using UnityEngine;
using System.Collections;

public class GrenadeLauncher : MonoBehaviour {

    public GameObject grenade;
    public int ammo = 20;
    public float rate;

    public float forwardVel;
    public float upwardVel;

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
                float Spread = 0.0f;
                GameObject go = Instantiate(grenade);
                go.transform.rotation = Random.rotation;
                go.transform.position = mg.gunModel.transform.position;
                Rigidbody rig = go.GetComponent<Rigidbody>();
                rig.velocity = mg.transform.right * forwardVel+ mg.transform.forward * upwardVel;
                rig.angularVelocity = Random.rotation.eulerAngles * 3;

                cool += 1;
                ammo--;
            }
        }
        cool -= Time.deltaTime * rate;
        cool = Mathf.Clamp01(cool);
        firing = false;

    }

    void Fire()
    {
        if (ammo > 0)
            firing = true;
    }
}
