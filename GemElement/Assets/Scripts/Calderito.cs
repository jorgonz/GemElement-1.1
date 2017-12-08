using UnityEngine;
using System.Collections;

public class Calderito : MonoBehaviour {

	// Public variables
	public GameObject player;
	public GameObject bullet;
	public float viewRadius;
	public float bulletSpeed;
	public float secondsToFire;
	public Sprite spriteRight;
	public Sprite spriteUp;
	public Sprite spriteLeft;
	public Sprite spriteDown;

	// Private variables
	private Rigidbody2D rbBullet;
	private Rigidbody2D rbPlayer;
	private Rigidbody2D rbBulletClone;
	private Vector2 position;
	private Vector2 bulletVelocity;
	private SpriteRenderer sr;
	private bool canShoot;

	// Constant value of pi/4
	const float pi4 = Mathf.PI / 4;

	// Use this for initialization
	void Start () {
		rbPlayer = player.GetComponent<Rigidbody2D> ();
		rbBullet = bullet.GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		position = (Vector2)this.transform.position;
		canShoot = true;
	}

	// Function that gets called every frame (even before Update).
	void FixedUpdate() {

        if(!GemController.objectsStopped)
        {
            changeSprite();
            StartCoroutine(Fire());
        }

		
	}

	// Function for changing the sprite of this GameObject.
	void changeSprite() {
		// Vector from the position of this GameObject to the position of the player.
		Vector2 pathToPlayer = rbPlayer.position - position;

		// Comparisons to change the sprite of this GameObject to the corresponding one
		// for the cases where the player is exactly to the right, left, or above or below 
		// this GameObject.
		if (pathToPlayer.x > position.x && pathToPlayer.y == position.y)
			sr.sprite = spriteRight;
		else if (pathToPlayer.x < position.x && pathToPlayer.y == position.y)
			sr.sprite = spriteLeft;
		else if (pathToPlayer.x == position.x && pathToPlayer.y > position.y)
			sr.sprite = spriteUp;
		else if (pathToPlayer.x == position.x && pathToPlayer.y < position.y)
			sr.sprite = spriteDown;


		else {
			// Positive angle of the vector that goes from the position of this GameObject
			// to the player.
			float angle = Mathf.Abs(Mathf.Atan (pathToPlayer.y / pathToPlayer.x));

			// Comparisons to check in what quadrant (relative to this GameObject) the player is
			// positioned. The sprite of this GameObject is changed according to whether the
			// player is more in the horizontal or vertical part of the quadrant found.
			if (rbPlayer.position.x > position.x && rbPlayer.position.y > position.y) {
				if (angle > pi4)
					sr.sprite = spriteUp;
				else
					sr.sprite = spriteRight;
			} else if (rbPlayer.position.x > position.x && rbPlayer.position.y < position.y) {
				if (angle > pi4)
					sr.sprite = spriteDown;
				else
					sr.sprite = spriteRight;
			} else if (rbPlayer.position.x < position.y && rbPlayer.position.y > position.y) {
				if (angle > pi4)
					sr.sprite = spriteUp;
				else
					sr.sprite = spriteLeft;
			} else {
				if (angle > pi4)
					sr.sprite = spriteDown;
				else
					sr.sprite = spriteLeft;
			}
		}
	}

	// Function that controls the shooting ability of this GameObject.
	IEnumerator Fire() {
		// Vector that goes from this GameObject's position to the player.
		bulletVelocity = rbPlayer.position - position;

		// Comparison to check whether this GameObject can shoot given its
		// shooting rate (how much time it needs for shooting again once it shoots)
		// and if the distance to the player is within the view radius of this GameObject.
		if (canShoot && (bulletVelocity.magnitude <= viewRadius)) {
			canShoot = false;

			// A bullet is created in the position of this GameObject.
			rbBulletClone = Instantiate (rbBullet);
			rbBulletClone.position = position;

			// Checks if the player is moving.
			if (rbPlayer.velocity != Vector2.zero) {
				// Get the unitary vector of the player's velocity and multiplies it by a
				// random value between the range [0.2, 1.0).
				Vector2 temp = rbPlayer.velocity.normalized;
				temp.Scale(new Vector2( Random.Range(0.2f, 1.0f), Random.Range(0.2f, 1.0f)));

				// The velocity of the bullet is added the previous vector to modify its
				// direction towards a predicted position of the player.
				bulletVelocity += temp;
			}

			// The velocity of the bullet is normalized and then scaled with the desired speed.
			bulletVelocity.Normalize ();
			bulletVelocity.Scale (new Vector2 (bulletSpeed, bulletSpeed));
			rbBulletClone.velocity = bulletVelocity;

			// The bullet is destroyed after 1 second.
			Destroy (rbBulletClone.gameObject, 3);

			// The program waits a predefined amount of seconds (secondToFire), and then this
			// GameObject can shoot again.
			yield return new WaitForSeconds (secondsToFire);
			canShoot = true;
		}
	}
}
