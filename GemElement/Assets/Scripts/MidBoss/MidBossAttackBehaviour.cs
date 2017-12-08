using UnityEngine;
using System.Collections;

public class MidBossAttackBehaviour : MonoBehaviour {

    //Store the Transform of the player
    private Transform trsPlayer;

    //Reference to the Firewall attack object
    public GameObject gbjFireWall;
    public GameObject gbjFireMarker;

    //Reference to the Lightning Bolt attack object
    public GameObject gbjLighting;

    //This is used to know when was the last time a firewall was spawned, and
    //to know when can we create a new one.
	public float fFireWallCd;
    private float fLastWall;
	public float fTimeBeforeWall;
	public float fWallDuration;

    //This is used to know when was the last time a lighting attack was spawned
    // and to know when can we create a new one.
    private float fLightingCd;
    private float fLastLightning;

    //This the current Boss phase, starts at one and goes up to 3, the boss
    //attacks more often if the phase is higher.
    public int iBossPhase;

	// Use this for initialization
	void Start () {

        iBossPhase = 3;
        fFireWallCd = 10.0f;
        fLastWall = -1.0f;
        trsPlayer = GameObject.FindGameObjectWithTag("Player").
            GetComponent<Transform>();

        fLightingCd = 2.0f;
        fLastLightning = Time.time;
	
	}
	
	// Update is called once per frame
	void Update () {

        //A firewall is constantly being spawned every set amount of time
        if(Time.time >= fLastWall + (fFireWallCd - iBossPhase))
        {
			//Start the fireWall
			StartFireWall ();

            //Set the time for the next firewall
            fLastWall = Time.time;
        }

        if(Time.time >= fLastLightning + fLightingCd)
        {
            int iAttack = Random.Range(0, iBossPhase + 1);

            if(iAttack > 0)
            {
				if (!GemController.objectsStopped)
                	StartLighting();
            }

            fLastLightning = Time.time;
        }
        

	}

    /// <summary>
    /// This function instantiates a marker that displays where the firewall
    /// appear, we do this to give time for the player to move and to give him
    /// feedback.
    /// </summary>
    void StartFireWall()
    {
        //Create the marker right on top of the player.
        GameObject gbjMarkerInstance = (GameObject) Instantiate(gbjFireMarker, 
            trsPlayer.position, Quaternion.identity);

        //Start the corutine that creates and destroys the wall.
        StartCoroutine(Firewall(trsPlayer.position,gbjMarkerInstance));
    }

    /// <summary>
    /// This coroutine creates and destroys the firewall after a set amount of
    /// time
    /// </summary>
    /// <param name="vc3PlayerPos"> The position of the player </param>
    /// <param name="gbjMarker"> The Marker GameObject </param>
    /// <returns></returns>
    IEnumerator Firewall(Vector3 vc3PlayerPos, GameObject gbjMarker)
    {
        //Wait before creating the firewall
		yield return new WaitForSeconds(fTimeBeforeWall);

        //Destroy the marker, we no longer need it
        Destroy(gbjMarker);

        //Create the Firewall
        GameObject gbjWallInstance = (GameObject) Instantiate(gbjFireWall, 
            vc3PlayerPos, Quaternion.identity);

        //Wait before destroying the wall
		yield return new WaitForSeconds(fWallDuration);

        //Destroy the wall  
        Destroy(gbjWallInstance);
        
    }

    void StartLighting()
    {
        float fHeight = this.transform.GetComponent<SpriteRenderer>().bounds.
            size.y;
        float fWidth = this.transform.GetComponent<SpriteRenderer>().bounds.
            size.x;

        Quaternion qtrInitalRot = Quaternion.Euler(0, 0, 0);

        Vector3 vc3IntialPos = this.transform.position;
        vc3IntialPos.y -= (fHeight / 2);

        switch (iBossPhase)
        {
            case 1:
                Instantiate(gbjLighting, vc3IntialPos , qtrInitalRot);
                break;
            case 2:

                vc3IntialPos.x -= fWidth/4;
                
                for(int i = 0; i<2; i++)
                {
                    vc3IntialPos.x += i * fWidth/2;
                    Instantiate(gbjLighting,vc3IntialPos, qtrInitalRot);
                    vc3IntialPos.x -= i * fWidth / 2;
                }

                break;
            case 3:

                vc3IntialPos.x -= fWidth/2;
                vc3IntialPos.y -= (fHeight / 2f) ;
                for (int i = 0; i < 3; i++)
                {
                    vc3IntialPos.x += i * fWidth / 2;
                    Instantiate(gbjLighting, vc3IntialPos, qtrInitalRot);
                    vc3IntialPos.x -= i * fWidth / 2;
                }

                break;
            default:
                Debug.LogError("MidBoss Phase isn't 1 or 2 or 3");
                break;
        }
    }
}
