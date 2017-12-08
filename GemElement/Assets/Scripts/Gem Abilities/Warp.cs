using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Warp : MonoBehaviour {

	private float lastWarpTime;
	public float warpCooldown;
	public float warpDuration;

    public GameObject gbjBlueRing;

    public AudioSource auWarp;

	void stopObjects(){
		GemController.objectsStopped = true;
	}

	void awakeObjects(){
		GemController.objectsStopped = false;
	}

	// Use this for initialization
	void Start () {
		lastWarpTime = 0;
	}
	
	// Update is called once per frame
	void Update () {


        if(!GameController.instance.isOnDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Space) && GemController.gem == GemController.ActiveGem.WARP && !GameController.instance.isOnDialogue)
            {
                if ((Time.timeSinceLevelLoad - lastWarpTime) > warpCooldown)
                {
                    lastWarpTime = Time.timeSinceLevelLoad;
                    auWarp.Play();
                    stopObjects();

                    GameObject gbj = (GameObject)Instantiate(gbjBlueRing, this.transform.position, Quaternion.identity);
                    gbj.transform.parent = this.transform;
                }


            }

            if (Time.timeSinceLevelLoad >= (lastWarpTime + warpDuration))
            {
                awakeObjects();
            }
        }

		
	}
}
