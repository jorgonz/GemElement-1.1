using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Pull : MonoBehaviour {

	private Vector2 pullTowards;
	public float maxDistance;
	public float pullSpeed;
	public float shootSpeed;
	public float rotateRadio;

    public GameObject gbjRingGreen;
 
	public static bool hasObjectRotating;

	public static GameObject objectCollided;

	public static bool checkCollision;

    public AudioSource auPull_Push;

	// Use this for initialization
	void Start () {
		checkCollision = false;
		maxDistance = 100f;
		pullSpeed = 4f;
		rotateRadio = 1.5f;
		hasObjectRotating = false;
		shootSpeed = 8;
	}

	/*
	 * Draw a ray towards the direction the player is facing. If the ray hits an object or a
	 * projectile, set it as the object to pull and calculate the direction in which it will
	 * get pulled.
	 */
	void setObjectToPull(){
	 	
		Vector2 rayStart;
		Vector2 rayDirection;
		int layerMask = 1 << 15;
		layerMask = ~layerMask;

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

		RaycastHit2D hit = Physics2D.Raycast (rayStart, rayDirection, maxDistance, layerMask);

		if(hit.transform != null)
			Debug.Log(hit.transform.gameObject.name);

		// If an object is detected, set variables
		if(hit.transform != null && !hasObjectRotating && (hit.transform.gameObject.tag == "Object" || hit.transform.gameObject.tag == "Projectile")){
			hit.transform.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
			if(objectCollided != null && objectCollided != hit.transform.gameObject){
				if (objectCollided.tag == "Object") {
					objectCollided.GetComponent<Rigidbody2D> ().isKinematic = true;
				}
			}

			objectCollided = hit.transform.gameObject;
			objectCollided.GetComponent<Rigidbody2D> ().isKinematic = true;
			pullTowards = transform.position - objectCollided.transform.position;
            pullTowards = pullTowards.normalized;
		}
	}

	/* 
	 * Avoids triggering the collision of an object right when it is
	 * pulled.
	 */
	IEnumerator switchCollisionBool(){
		checkCollision = true;
		yield return new WaitForSeconds (0.10f);
		checkCollision = false;
	}

	/*
	 * Pull the desired ojbect towards a direction.
	 */
	void pullObject(){
		objectCollided.GetComponent<Rigidbody2D> ().isKinematic = false;
		StartCoroutine (switchCollisionBool());
		objectCollided.GetComponent<Rigidbody2D> ().velocity += (pullTowards * pullSpeed * Time.deltaTime);
	}

	/*
	 * Pull a desired object until it has reached a certain distance from the player.
	 */
	void pullUntilDistance(){
		float distance = Vector2.Distance (transform.position, objectCollided.transform.position);
		//Debug.Log (distance);

		if (!hasObjectRotating) {
			if (distance > rotateRadio) {
				pullObject ();
			}
			else {
				hasObjectRotating = true;
				objectCollided.transform.rotation = Quaternion.Euler (Vector3.Cross (objectCollided.transform.position - this.transform.position, Vector3.forward).normalized);
				objectCollided.GetComponent<Collider2D> ().enabled = false;
				objectCollided.GetComponent<Rigidbody2D> ().isKinematic = true;
			}
		}
	}

	/*
	 * Rotate a projectile around the player.
	 */
	void rotateAround(){
		if (hasObjectRotating){
			objectCollided.GetComponent<Rigidbody2D> ().isKinematic = false;
			objectCollided.transform.SetParent (this.transform);
			objectCollided.transform.RotateAround (objectCollided.transform.parent.transform.position, Vector3.forward * -1, 135 * Time.deltaTime);
		}
	}

	void shootObject(){
		Vector3 shootDirection3 = Vector3.Cross (objectCollided.transform.position - this.transform.position, Vector3.forward).normalized;
		Vector2 shootDirection = new Vector2(shootDirection3.x, shootDirection3.y);
		if (hasObjectRotating) {
			hasObjectRotating = false;
			objectCollided.GetComponent<Collider2D> ().enabled = true;
			objectCollided.transform.SetParent (null);
			objectCollided.GetComponent<Rigidbody2D> ().velocity = (shootDirection * shootSpeed);
            objectCollided.gameObject.tag = "PlayerProjectile";
			objectCollided = null;
		}
	}

	// Update is called once per frame
	void Update () {

        if(!GameController.instance.isOnDialogue)
        {
            if (GemController.gem == GemController.ActiveGem.PULL_PUSH)
            {
                // Activate pull
                if (Input.GetKeyDown(KeyCode.O))
                {

                    setObjectToPull();

                    GameObject gbj = (GameObject)Instantiate(gbjRingGreen, this.transform.position, Quaternion.identity);

                    gbj.transform.parent = this.transform;

                    //audioFiles.sounds ["pull_push"].Play ();
                    auPull_Push.Play();
                }

                // If there's an object that's going to be pulled...
                if (objectCollided != null)
                {
                    // ... pull it
                    if (objectCollided.tag == "Object")
                        pullObject();
                    // ... pull it and rotate it around the player.
                    else if (objectCollided.tag == "Projectile")
                    {
                        pullUntilDistance();
                        rotateAround();
                    }
                }

                // Shoot projectile
                if (Input.GetKeyDown(KeyCode.O))
                {
                    if (objectCollided != null && objectCollided.tag == "Projectile" && hasObjectRotating)
                    {
                        shootObject();
                    }
                }
            }
        }

		
	}
}
