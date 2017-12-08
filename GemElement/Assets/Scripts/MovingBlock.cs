using UnityEngine;
using System.Collections;

public class MovingBlock : MonoBehaviour {


    public int iDir;
    public int iSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 vc3BlockPos = this.transform.position;

        if(!GemController.objectsStopped)
        {
            if (iDir == 0)
            {
                vc3BlockPos.y -= iSpeed * Time.deltaTime;
            }
            else if (iDir == 1)
            {
                vc3BlockPos.x += iSpeed * Time.deltaTime;
            }
        }


  

        this.transform.position = vc3BlockPos;
	}
}
