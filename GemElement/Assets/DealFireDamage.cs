using UnityEngine;
using System.Collections;

public class DealFireDamage : MonoBehaviour {

    float fTimetoWait;
    float fTimeStamp;
    bool bCanDamage;

	// Use this for initialization
	void Start () {

        fTimetoWait = 0.5f;
        fTimeStamp = Time.time;
        bCanDamage = true;
	
	}
	
	// Update is called once per frame
	void Update () {

        if(Time.time >= fTimeStamp + fTimetoWait)
        {
            fTimeStamp = Time.time;
            bCanDamage = true;
        }
	
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if(bCanDamage && col.transform.tag == "Player")
        {
            PlayerController.instance.decreaseLife();
            bCanDamage = false;
        }

    }


}
