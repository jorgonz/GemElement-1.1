using UnityEngine;
using System.Collections;

public class Push : MonoBehaviour {

	public float pushSpeed;
	public float maxDistance;

    public GameObject gbjRingGreen;

    private Vector2 pushTowards;

	public static GameObject objectPushed;

    public AudioSource auPull_Push;

	void setObject(){
		Vector2 rayStart;
		Vector2 rayDirection;

		// Calculate ray direction
		if (movement.direction == movement.directionEnum.RIGHT){
			rayStart = new Vector2 (transform.position.x + 1.5f, transform.position.y);
			rayDirection = Vector2.right;
		}
		else if (movement.direction == movement.directionEnum.LEFT){
			rayStart = new Vector2 (transform.position.x - 1.5f, transform.position.y);
			rayDirection = Vector2.left;
		}
		else if (movement.direction == movement.directionEnum.UP){
			rayStart = new Vector2 (transform.position.x, transform.position.y + 1.5f);
			rayDirection = Vector2.up;
		}
		else {
			rayStart = new Vector2 (transform.position.x, transform.position.y - 1.5f);
			rayDirection = Vector2.down;
		}

		RaycastHit2D hit = Physics2D.Raycast (rayStart, rayDirection, maxDistance);

		if (hit.transform != null){
			if (hit.transform.gameObject.tag == "Projectile"){
				Destroy (hit.transform.gameObject);
			}
			else if (hit.transform.gameObject.tag == "Object"){
				objectPushed = hit.transform.gameObject;
				objectPushed.GetComponent<Rigidbody2D> ().isKinematic = false;
				pushTowards = (objectPushed.transform.position - transform.position);
			}
		}
	}

	void pushObject(){
		if (objectPushed != null && objectPushed.tag == "Object"){
			objectPushed.GetComponent<Rigidbody2D> ().velocity += (pushTowards * pushSpeed * Time.deltaTime);
		}
	}

	// Use this for initialization
	void Start () {
		pushSpeed = 4.0f;
		maxDistance = 100f;
	}
	
	// Update is called once per frame
	void Update () {

        if(!GameController.instance.isOnDialogue)
        {
            if (GemController.gem == GemController.ActiveGem.PULL_PUSH /*&& !GameController.instance.isOnDialogue*/)
            {
                if (Input.GetKeyDown(KeyCode.P))
                {

                    setObject();

                    GameObject gbj = (GameObject)Instantiate(gbjRingGreen, this.transform.position, Quaternion.identity);

                    gbj.transform.parent = this.transform;

                    //audioFiles.sounds ["pull_push"].Play ();
                    auPull_Push.Play();
                }

                if (objectPushed != null)
                {
                    pushObject();
                }
            }
        }

		
	}
}
