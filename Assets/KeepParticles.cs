using UnityEngine;
using System.Collections;

public class KeepParticles : MonoBehaviour {

    public ParticleSystem PS;

	// Use this for initialization
	void Start () {
        if (PS == null)
            PS = GetComponent<ParticleSystem>();
	}

    void Impact(ImpactData id)
    {
        PS.Stop();
        PS.transform.parent = null;
        Destroy(PS.gameObject, PS.startLifetime); // if particles live for at most 5 secs
    }
}
