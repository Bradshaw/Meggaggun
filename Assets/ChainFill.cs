using UnityEngine;
using System.Collections;

public class ChainFill : MonoBehaviour {

    public ChainGun l;

    RectTransform rt;

    // Use this for initialization
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rt.sizeDelta = new Vector2(((float)l.ammo / (float)l.maxAmmo) * 90.0f, rt.rect.height);
    }
}
