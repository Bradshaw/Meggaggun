﻿using UnityEngine;
using System.Collections;

public class NadeFill : MonoBehaviour {

    public GrenadeLauncher l;

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
