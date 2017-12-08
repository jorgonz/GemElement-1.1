using UnityEngine;
using System.Collections;

public class ArrowDeathTimer : MonoBehaviour {


    public float TTL = 5.0f;
    private float fTimeStamp;
	
    // Use this for initialization
	void Start () {

        fTimeStamp = Time.time;
	
	}
	
	// Update is called once per frame
	void Update () {

        if(Time.time >= TTL + fTimeStamp)
        {
            Destroy(this.gameObject);
        }
	
	}
}
