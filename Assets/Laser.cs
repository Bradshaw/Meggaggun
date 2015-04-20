using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

    public AudioSource src;

    public Mesh inner;
    public Mesh outer;
    public Mesh imp1;
    public Mesh imp2;
    public Material inMat;
    public Material outMat;
    public float pieceLength;

    public Projectile dummyProj;
    public LayerMask lm;
    public GameObject mg;

    public int ammo = 500;
    public int maxAmmo = 500;

    public float DPS;
    float secondsToDeep = 0;

    public ParticleSystem PS;

    Vector3 lastHit = Vector3.zero;

    bool firing = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        ammo = Mathf.Clamp(ammo, 0, maxAmmo);
        secondsToDeep = Mathf.Max(0, secondsToDeep - Time.deltaTime * DPS);
        if (firing)
        {
            if (!src.isPlaying)
                src.Play();
            Ray r = new Ray(mg.transform.position, mg.transform.forward);
            RaycastHit rh;
            if (Physics.Raycast(r, out rh, Mathf.Infinity, lm))
            {
                if (secondsToDeep <= 0)
                {
                    dummyProj.damage = 1;
                    ammo--;
                }
                else
                    dummyProj.damage = 0;
                ImpactData impact;
                impact.projectile = dummyProj;
                impact.rayhit = rh;
                impact.target = rh.collider.gameObject;
                lastHit = rh.point;
                rh.collider.transform.root.gameObject.SendMessage("Impact", impact, SendMessageOptions.DontRequireReceiver);
                PS.transform.position = rh.point + rh.normal * 0.1f;
                PS.transform.rotation = Quaternion.LookRotation(rh.normal);
                PS.enableEmission = true;
                PS.emissionRate = 50;
                PS.Emit(3);
            }
        }
        else
        {
            src.Stop();
            PS.enableEmission = false;
        }
	}

    void LateUpdate()
    {
        //firing = false;
    }

    void Fire()
    {
        if (ammo>0)
            firing = true;
    }

    void OnRenderObject()
    {
        if (pieceLength > 0 && firing)
        {
            Vector3 line = lastHit - mg.transform.position;
            Vector3 step = line.normalized;
            Quaternion rot = Quaternion.LookRotation(line);
            rot = Quaternion.AngleAxis(Time.time * -186.4f, step) * rot;
            //*
            inMat.SetPass(0);
            for (float i = 0; i <= line.magnitude; i += pieceLength)
            {
                rot = Quaternion.AngleAxis(13.659f, step) * rot;
                Graphics.DrawMeshNow(inner, mg.transform.position + step * i, rot);
            }
            rot = Quaternion.LookRotation(line);
            rot = Quaternion.AngleAxis(Time.time * -463.4f, step) * rot;
            Graphics.DrawMeshNow(imp1, mg.transform.position, rot);
            rot = Quaternion.AngleAxis(Time.time * -444.894f, step) * Quaternion.LookRotation(-line);
            Graphics.DrawMeshNow(imp2, mg.transform.position + line, rot);
            //*/

            //*
            outMat.SetPass(0);
            rot = Quaternion.AngleAxis(Time.time * 336.4f, step) * Quaternion.LookRotation(-line);
            Graphics.DrawMeshNow(imp1, mg.transform.position + line, rot);
            rot = Quaternion.LookRotation(line);
            rot = Quaternion.AngleAxis(Time.time * 360, step) * rot;
            Graphics.DrawMeshNow(imp2, mg.transform.position, rot);
            for (float i = 0; i <= line.magnitude; i += pieceLength)
            {
                rot = Quaternion.AngleAxis(-16.351f, step) * rot;
                Graphics.DrawMeshNow(outer, mg.transform.position + step * i, rot);
            }
            //*/
            firing = false;
        }
    }

}

