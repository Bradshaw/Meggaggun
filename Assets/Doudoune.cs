using UnityEngine;
using System.Collections;

enum AIPhase
{
    idle,
    tactical,
    attack
}

public class Doudoune : MonoBehaviour {

    public float runSpeed;
    public float bulletCooldown = 0.4f;

    SpriteRenderer _sr;
    public Sprite idle;
    public Sprite aim;
    public Sprite fire;
    public Sprite tactical;

    AIPhase phase = AIPhase.idle;

    GameObject player;
    public int startHitPoints = 100;
    int hitPoints;

    float nextPhaseIn = 0;
    float bullets;
    float bulletCool;

    Vector2 direction;
    Rigidbody2D _rig;

	// Use this for initialization
	void Start () {
        _sr = GetComponentInChildren<SpriteRenderer>();
        _rig = GetComponent<Rigidbody2D>();
        hitPoints = startHitPoints;
        player = FindObjectOfType<DoomGuyMovement>().gameObject;
        changePhase();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (nextPhaseIn <= 0)
        {
            changePhase();
        }
        switch (phase)
        {
            case AIPhase.idle:
                _rig.AddForce(direction * runSpeed);
                nextPhaseIn -= Time.fixedDeltaTime;
                break;
            case AIPhase.tactical:
                Vector2 runDir = (direction - transform.position.xy());
                if (runDir.magnitude>0.25)
                    _rig.AddForce(runDir.normalized * runSpeed);
                nextPhaseIn -= Time.fixedDeltaTime;
                break;
            case AIPhase.attack:
                bulletCool -= Time.fixedDeltaTime;
                if (bulletCool <= 0)
                {
                    if (bullets > 0)
                    {
                        
                        bullets -= 1;
                        bulletCool = bulletCooldown;
                        _sr.sprite = fire;
                    }
                    else
                    {
                        nextPhaseIn = -1;
                    }
                }
                else if (bulletCool < 3*(bulletCooldown / 4))
                {
                    _sr.sprite = aim;
                }
                break;
        }
	}

    void OnDrawGizmos()
    {
    }

    void changePhase()
    {
        if (phase == AIPhase.tactical)
        {
            Ray r = new Ray(transform.position, player.transform.position - transform.position);
            RaycastHit rh;
            if (Physics.Raycast(r, out rh))
            {
                if (rh.collider.name == "Dude3D")
                    phase = AIPhase.attack;
            }
            else
            {
                phase = AIPhase.idle;
            }
        }
        else
        {
            Ray r = new Ray(transform.position, player.transform.position - transform.position);
            RaycastHit rh;
            if (Physics.Raycast(r, out rh))
            {
                if (rh.collider.name == "Dude3D")
                    phase = AIPhase.tactical;
            }
            else
            {
                phase = AIPhase.idle;
            }
        }
        Vector2 fromPlayer;
        switch (phase)
        {
            case AIPhase.idle:
                float angle = Random.value * Mathf.PI * 2;
                direction = getFurthest();
                nextPhaseIn = 2.0f + Random.value * 3.0f;
                _sr.sprite = idle;
                break;
            case AIPhase.tactical:
                fromPlayer = transform.position.xy() - player.transform.position.xy();
                fromPlayer = fromPlayer.normalized * (0.5f + Random.value*1.5f);
                direction = player.transform.position.xy() + fromPlayer;
                nextPhaseIn = 1 + Random.value * 2;
                _sr.sprite = tactical;
                break;
            case AIPhase.attack:
                fromPlayer = transform.position.xy() - player.transform.position.xy();
                fromPlayer = fromPlayer.normalized * (0.5f + Random.value*1.5f);
                direction = player.transform.position.xy() + fromPlayer;
                bulletCool = bulletCooldown;
                bullets = 3;
                _sr.sprite = aim;
                nextPhaseIn = 1;
                break;
        }
    }

    Vector2 getFurthest()
    {
        Vector2 res = Vector2.zero;
        for (int i = 0; i < 8; i++)
        {
            Ray r = new Ray(transform.position, Quaternion.Euler( 0, 0, (360/8)*i )*Vector3.up);
            RaycastHit rh;
            if (Physics.Raycast(r, out rh))
            {
                if (rh.distance > res.magnitude)
                {
                    res = rh.point.xy() - transform.position.xy();
                }
            }
        }
        return res.normalized ;
    }

}
