using UnityEngine;
using System.Collections;

public class MidBossMoveBehaviour : MonoBehaviour {

    Vector3 vc3MidBossPos;

    int iDir;

    float fMovDuration;

    float fTimeForNextDir;
    float fLastPicked;

    public GameObject gbjFireWall;

    int Hp;

	// Use this for initialization
	void Start () {

        fTimeForNextDir = 0.0f;
        fLastPicked = -1.0f;

        vc3MidBossPos = this.transform.position;

        Hp = 3;
        
        
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time >= fTimeForNextDir + fLastPicked)
        {
            PickDirandDur();
            fLastPicked = Time.time;
            fTimeForNextDir = fMovDuration;
        }

		if (!GemController.objectsStopped) {
			if (iDir == 0)
				MoveLeft ();
			else
				MoveRight ();
		}


        if(Hp<=0)
        {
            StartCoroutine(EndThis());
            //Destroy(this.gameObject);
        }

        this.transform.position = vc3MidBossPos;
	}

    IEnumerator EndThis()
    {
        Vector3 vc3Pos = this.transform.position;
        Instantiate(gbjFireWall, vc3Pos, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        vc3Pos.y += 2.0f;
        Instantiate(gbjFireWall,vc3Pos , Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        vc3Pos.y -= 4.0f;
        Instantiate(gbjFireWall, vc3Pos, Quaternion.identity);

        Destroy(this.gameObject);

        Application.LoadLevel("MenuOPOP");
    }



    void MoveLeft()
    {
        vc3MidBossPos.x -= 1.5f * Time.deltaTime;
    }

    void MoveRight()
    {
        vc3MidBossPos.x += 1.5f * Time.deltaTime;
    }

    void PickDirandDur()
    {   
        iDir = Random.Range(0, 2);

        fMovDuration = Random.Range(1, 2);
    }

    void OnTriggerEnter2D(Collider2D c2dOther)
    {
        if(c2dOther.gameObject.tag == "RightBound")
        {
            Debug.Log("Forced Left");
            ForceLeft();
        }
        else if(c2dOther.gameObject.tag == "LeftBound")
        {
            Debug.Log("Forced Right");
            ForceRight();
        }
        else if(c2dOther.gameObject.tag == "PlayerProjectile")
        {
            Destroy(c2dOther.gameObject);
            Debug.Log("Deal damage");
            Hp -= 1;
        }


    }

    void ForceRight()
    {
        iDir = 1;
        fMovDuration = 3.0f;
        fLastPicked = Time.time;
        fTimeForNextDir = fMovDuration;
    }

    void ForceLeft()
    {
        iDir = 0;
        fMovDuration = 3.0f;
        fLastPicked = Time.time;
        fTimeForNextDir = fMovDuration;
    }


}
