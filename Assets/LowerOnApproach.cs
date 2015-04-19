﻿using UnityEngine;
using System.Collections;

public class LowerOnApproach : MonoBehaviour {

    Transform player;
    public Transform lowerThis;
    public float lowerBy = 0.5f;
    public float distance = 5;
    public float speed = 1;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<DoomGuyMovement>().transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, player.position) < distance)
        {
            Vector3 lpos = lowerThis.localPosition;
            lpos.z = Mathf.Lerp(lpos.z, lowerBy, speed * Time.deltaTime);
            lowerThis.localPosition = lpos;
        }
        else
        {
            Vector3 lpos = lowerThis.localPosition;
            lpos.z = Mathf.Lerp(lpos.z, 0, speed * Time.deltaTime);
            lowerThis.localPosition = lpos;
        }
	}
}