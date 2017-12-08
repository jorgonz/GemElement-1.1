using UnityEngine;
using System.Collections;

public class ShootBlock : MonoBehaviour {

    public GameObject gbjMovingBlock;

    public int iDir;

    private float TimeStamp;
    public float fCooldown;

	// Use this for initialization
	void Start () {

        TimeStamp = Time.time;
       
	
	}
	
	// Update is called once per frame
	void Update () {

        if(Time.time >= TimeStamp + fCooldown)
        {
            Shoot();
            TimeStamp = Time.time;
        }


    }

    void Shoot()
    {
        if (iDir == 0)
        {
            GameObject gbjAux = (GameObject)Instantiate(gbjMovingBlock, this.transform.position, Quaternion.identity);

            gbjAux.GetComponent<MovingBlock>().iDir = 0;
        }
        else if (iDir == 1)
        {
            GameObject gbjAux = (GameObject)Instantiate(gbjMovingBlock, this.transform.position, Quaternion.identity);

            gbjAux.GetComponent<MovingBlock>().iDir = 1;
        }
    }

}
