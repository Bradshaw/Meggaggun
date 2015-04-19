using UnityEngine;
using System.Collections;

public class LowerOnApproach : MonoBehaviour {

    public Transform lowerThis;
    public float lowerBy = 0.5f;
    public float speed = 1;

    public float stayOpenForSeconds;

    float timeToClose = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timeToClose -= Time.fixedDeltaTime;
        if (timeToClose>0)
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

    void OnTriggerStay(Collider other)
    {
        timeToClose = stayOpenForSeconds;
    }
}
