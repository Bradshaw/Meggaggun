using UnityEngine;
using System.Collections;

public class ParticlesOnDeath : MonoBehaviour {

    public string byName;
    public ParticleSystem PS;
    public int count;

    void Impact(ImpactData id)
    {
        if (byName.Length > 0)
            PS = GameObject.Find(byName).GetComponent<ParticleSystem>();
        PS.transform.position = id.rayhit.point+id.rayhit.normal*0.1f;
        PS.transform.rotation = Quaternion.LookRotation(id.rayhit.normal);
        PS.Emit(count);

    }



}
