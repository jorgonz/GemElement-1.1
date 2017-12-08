using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Blink : MonoBehaviour {

	public static Vector3 posAtBlink;

	public GameObject blink_checker;

	private float[] blinkDistance;

	private int blinksDone;

    public AudioSource auBlink;

    public GameObject gbjYellowRing;

    public RawImage feedback;
    private Texture[] textures;
    public Texture low;
    public Texture mid;
    public Texture high;

    void dash(){


		Vector3 dashTowards = Vector3.forward;

		//check_blink_target script = checker.GetComponent<check_blink_target> ();
		if (movement.direction == movement.directionEnum.LEFT){
			dashTowards = new Vector3 (transform.position.x - blinkDistance [blinksDone % blinkDistance.Length], transform.position.y, 0);
		}
		else if (movement.direction == movement.directionEnum.RIGHT){
			dashTowards = new Vector3 (transform.position.x + blinkDistance [blinksDone % blinkDistance.Length], transform.position.y, 0);
		}
		else if (movement.direction == movement.directionEnum.DOWN){
			dashTowards = new Vector3 (transform.position.x, transform.position.y - blinkDistance [blinksDone % blinkDistance.Length], 0);
		}
		else if (movement.direction == movement.directionEnum.UP){
			dashTowards = new Vector3 (transform.position.x, transform.position.y + blinkDistance [blinksDone % blinkDistance.Length], 0);
		}
		//GameObject checker = (GameObject)Instantiate (blink_checker, transform.position, transform.rotation);
		if (!Physics2D.OverlapCircle(dashTowards,0.08f)) {
			//blinksDone++;
			transform.position = dashTowards;
		}

		//Debug.Log (script.can_blink);
	}

	// Use this for initialization
	void Start () {
		
		blinkDistance = new float[] { 2f, 3f, 4f };
		blinksDone = 0;

        textures = new Texture[3];
        textures[0] = low;
        textures[1] = mid;
        textures[2] = high;

        feedback = GameObject.Find("BlinkFeedBack").GetComponent<RawImage>();
        feedback.enabled = false;
    }

	// Update is called once per frame
	void Update () {

        if(!GameController.instance.isOnDialogue)
        {
            if (GemController.gem == GemController.ActiveGem.BLINK)
            {
                feedback.enabled = true;
            }
            else
            {
                feedback.enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.Space) && GemController.gem == GemController.ActiveGem.BLINK)
            {
                dash();
                feedback.texture = textures[++blinksDone % blinkDistance.Length];
                //blinksDone++;
                GameObject gbj = (GameObject)Instantiate(gbjYellowRing, this.transform.position, Quaternion.identity);
                gbj.transform.parent = this.transform;

                //audioFiles.sounds ["blink"].Play ();
                auBlink.Play();
            }
        }

        
	}
}
