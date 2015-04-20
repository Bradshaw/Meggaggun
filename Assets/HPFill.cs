using UnityEngine;
using System.Collections;

public class HPFill : MonoBehaviour {

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

    RectTransform rt;

    // Use this for initialization
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Dude d = player.GetComponent<Dude>();
        if (d!=null)
            rt.sizeDelta = new Vector2(((float) d.hitpoints / 100.0f)*90.0f, rt.rect.height);
        else
            rt.sizeDelta = new Vector2(0, rt.rect.height);
    }
}
