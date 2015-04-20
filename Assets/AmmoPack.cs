using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ammoType
{
    laser = 0,
    rocket,
    nade,
    chain
}

public class AmmoPack : MonoBehaviour {

    public AudioSource src;

    GameObject _player;
    GameObject player
    {
        get
        {
            DoomGuyMovement dgm;
            CamStrap cs;
            if (_player == null)
            {
                dgm = FindObjectOfType<DoomGuyMovement>();
                if (dgm != null)
                    _player = dgm.gameObject;
                if (_player == null)
                {
                    cs = FindObjectOfType<CamStrap>();
                    if (cs != null)
                        _player = cs.gameObject;
                }
            }

            return _player;

        }
    }

    public SpriteRenderer display;
    public Sprite laser;
    public Sprite rocket;
    public Sprite nade;
    public Sprite chain;

    public float PickupDistance;


    ammoType at;

	// Use this for initialization
	void Start () {
        at = (ammoType) Random.Range(0, 4);
        switch (at)
        {
            case ammoType.laser:
                display.sprite = laser;
                break;
            case ammoType.rocket:
                display.sprite = rocket;
                break;
            case ammoType.nade:
                display.sprite = nade;
                break;
            case ammoType.chain:
                display.sprite = chain;
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector2.Distance(transform.position.xy(), player.transform.position.xy()) < PickupDistance)
        {
            AudioSource.PlayClipAtPoint(src.clip, this.transform.position);
            switch (at)
            {
                case ammoType.laser:
                    player.GetComponent<Laser>().ammo += 250;
                    break;
                case ammoType.rocket:
                    player.GetComponent<Rocket>().ammo += 5;
                    break;
                case ammoType.nade:
                    player.GetComponent<GrenadeLauncher>().ammo += 5;
                    break;
                case ammoType.chain:
                    player.GetComponent<ChainGun>().ammo += 125;
                    break;
            }
            Destroy(this.gameObject);
        }
	}
}
