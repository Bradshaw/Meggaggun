using UnityEngine;
using System.Collections;

public struct ImpactData
{
    public RaycastHit rayhit;
    public Projectile projectile;
    public GameObject target;
}

public class Projectile : MonoBehaviour {

    AudioSource src;

    public LayerMask lm;
    public GameObject FiredBy;
    public int damage = 0;

    Vector3 _velocity;
    public Vector3 velocity
    {
        set { _velocity = value; }
        get { return _velocity; }
    }

	// Use this for initialization
	void Start () {
        src = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Ray r = new Ray(transform.position, velocity*Time.fixedDeltaTime);
        RaycastHit rh;
        if (Physics.Raycast(r, out rh, Mathf.Infinity, lm))
        {
            if (rh.collider.transform.root.gameObject!=FiredBy && rh.distance < (velocity * Time.fixedDeltaTime).magnitude)
            {
                ImpactData impact;
                impact.projectile = this.gameObject.GetComponent<Projectile>();
                impact.rayhit = rh;
                impact.target = rh.collider.gameObject;
                rh.collider.transform.root.gameObject.SendMessage("Impact", impact, SendMessageOptions.DontRequireReceiver);
                this.gameObject.SendMessage("Impact", impact, SendMessageOptions.DontRequireReceiver);
                StartCoroutine(DestroyNextFrame());
                if (src!=null)
                    AudioSource.PlayClipAtPoint(src.clip, this.transform.position);
            }
        }
        transform.position = transform.position + velocity * Time.fixedDeltaTime;
	}

    IEnumerator DestroyNextFrame()
    {
        yield return new WaitForEndOfFrame();
        Destroy(this.gameObject);
    }
}
