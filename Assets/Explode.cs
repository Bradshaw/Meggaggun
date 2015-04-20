using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {
    public AudioSource src;

    public ParticleSystem particles;
    public Projectile projectile;
    public int count;
    public float minVelocity;
    public float maxVelocity;

    void Impact(ImpactData id)
    {
        BlowUp();
    }

    void BlowUp()
    {
        particles.Emit(500);
        AudioSource.PlayClipAtPoint(src.clip, this.transform.position);
        particles.transform.parent = null;
        Destroy(particles.gameObject, particles.startLifetime); // if particles live for at most 5 secs
        for (int i = 0; i < count; i++)
        {
            Projectile prj = Instantiate<Projectile>(projectile);
            prj.transform.position = this.transform.position;
            prj.transform.rotation = Random.rotation;
            prj.velocity = prj.transform.forward * Mathf.Lerp(minVelocity,maxVelocity,Random.value);


        }
    }

}
