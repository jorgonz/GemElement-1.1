using UnityEngine;
using System.Collections;

public class LightingMovement : MonoBehaviour {

    private Vector3 vc3Dir;

    private float fBulletSpeed;

    // private float fTTL;
    // private float fTimeStamp;

    //Store the Transform of the player
    private Transform trsPlayer;

    //Store the Transfrom of the Boss
    private Transform trsMidBoss;

    // Use this for initialization
    void Start()
    {
        trsPlayer = GameObject.FindGameObjectWithTag("Player").transform;

        fBulletSpeed = 1.0f;
        // fTTL = 4.0f;
        // fTimeStamp = Time.time + fTTL;

        vc3Dir = trsPlayer.position - this.transform.position;

        vc3Dir = vc3Dir.normalized;

    }

    // Update is called once per frame
    void Update()
    {
		if (this.gameObject != Pull.objectCollided) {
			if (!GemController.objectsStopped)
				this.transform.Translate (vc3Dir * fBulletSpeed * Time.deltaTime, Space.World);
		}

       
        
		/*
        if (Time.time >= fTimeStamp)
            Destroy(this.gameObject);
		*/
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        if(other.transform.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
