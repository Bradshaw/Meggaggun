﻿using UnityEngine;
using System.Collections;

public class DestroyAfterSeconds : MonoBehaviour {

    public float seconds;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, seconds);
	}
}
