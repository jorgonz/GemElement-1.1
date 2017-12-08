using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

	// Public variables (values be changed in the inspector).
	public float moveSpeed;
	/*
	public Sprite spriteMovingU;
	public Sprite spriteMovingD;
	public Sprite spriteMovingL;
	public Sprite spriteMovingR;
	public Sprite spriteIdleU;
	public Sprite spriteIdleD;
	public Sprite spriteIdleL;
	public Sprite spriteIdleR;
	*/

	//public int direction; // values: 0 (right), 1 (up), 2 (left), 3 (down).
	public enum directionEnum {RIGHT, UP, LEFT, DOWN}
	public static directionEnum direction;

	// Private variables.
	private int moveX;
	private int moveY;
	private SpriteRenderer sr;
	private Rigidbody2D rb;
	private Animator this_anim; //Animator of the player (the object where is this script)
	private int iLastRunAnimValue = 1;
	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		rb = GetComponent<Rigidbody2D> ();
		//direction = 3;
		direction = directionEnum.DOWN;

		this_anim = GetComponent<Animator> (); //Animator of this object set to "this_anim"
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Blink"){
			GemController.blinkObtained = true;
			Destroy (other.gameObject);
		}
        else if(other.gameObject.tag == "Warp")
        {
            GemController.warpObtained = true;
            Destroy(other.gameObject);
        }
		/*else if (other.gameObject.tag == "Pad"){
            Camera.main.orthographicSize = 3;
            Application.LoadLevel ("OverWorld2");
		}
        else if (other.gameObject.tag == "Pad2")
        {
            Camera.main.orthographicSize = 3;
            Application.LoadLevel("Overworld3");
        }*/
	}

	// Update is called once per frame
	void FixedUpdate () {

        
            checkMovement();
            animate();
        
		
		//changeSprite ();

	}

	//Function that moves the object depending on the key pressed
	void checkMovement(){
		moveX = 0;
		moveY = 0;

		//Decide on which direction to move
		//Priorities: Up > Down ; Left > Right

		if (Input.GetKey (KeyCode.A) && !GameController.instance.isOnDialogue) { //Move left
			moveX = -1;
		}else if (Input.GetKey (KeyCode.D) && !GameController.instance.isOnDialogue) { //Move right
			moveX = 1;
		}

		if (Input.GetKey (KeyCode.W) && !GameController.instance.isOnDialogue) { //Move up
			moveY = 1;
		}else if (Input.GetKey (KeyCode.S) && !GameController.instance.isOnDialogue) {//Move down
			moveY = -1;
		}


		//Do the actual movement
		if (moveX != 0 || moveY != 0) { //If there is direction set, do the movement
			Vector2 velocity = new Vector2 (moveX * moveSpeed, moveY * moveSpeed);
			velocity.Normalize ();
			velocity.Scale (new Vector2 (moveSpeed, moveSpeed));
			rb.velocity = velocity;
		} else //Otherwise, stop
			rb.velocity = Vector2.zero;
	}

	void animate(){

		//Decide which animation to play
		//Priorities: Left > Right > Up > Down 

		//Animator variables
		//	Running (run):
		//		down = 1
		//		up = 2
		//		left = 3
		//		right = 4
		//	IDLE (IDLE)
		//		down = 1
		//		Up = 2
		//		left = 3
		//		right = 4



		//When pressing a key to run
		if (Input.GetKey (KeyCode.A) && !GameController.instance.isOnDialogue) {         //play running_left
			this_anim.SetInteger ("IDLE", 0);
			this_anim.SetInteger ("Run", 3);
			iLastRunAnimValue = 3;
			direction = directionEnum.LEFT;
		} else if (Input.GetKey (KeyCode.D) && !GameController.instance.isOnDialogue) {   //play running_right
			this_anim.SetInteger ("IDLE", 0);
			this_anim.SetInteger ("Run", 4);
			iLastRunAnimValue = 4;
			direction = directionEnum.RIGHT;
		} else if (Input.GetKey (KeyCode.W) && !GameController.instance.isOnDialogue) {   //play running_up
			this_anim.SetInteger ("IDLE", 0);
			this_anim.SetInteger ("Run", 2);
			iLastRunAnimValue = 2;
			direction = directionEnum.UP;
		} else if (Input.GetKey (KeyCode.S) && !GameController.instance.isOnDialogue) {   //play running_down
			this_anim.SetInteger ("IDLE", 0);
			this_anim.SetInteger ("Run", 1);
			iLastRunAnimValue = 1;
			direction = directionEnum.DOWN;
		} else {
			//When releasing a key to stop
			if (iLastRunAnimValue == 3) {       //play IDLE_left
				this_anim.SetInteger("Run", 0);
				this_anim.SetInteger("IDLE", 3);
				direction = directionEnum.LEFT;
			}else if (iLastRunAnimValue == 4) { //play IDLE_right
				this_anim.SetInteger("Run", 0);
				this_anim.SetInteger("IDLE", 4);
				direction = directionEnum.RIGHT;
			}else if (iLastRunAnimValue == 2) { //play IDLE_up
				this_anim.SetInteger("Run", 0);
				this_anim.SetInteger("IDLE", 2);
				direction = directionEnum.UP;
			}else { //play IDLE_down
				this_anim.SetInteger("Run", 0);
				this_anim.SetInteger("IDLE", 1);
				direction = directionEnum.DOWN;
			}
		}



	}

	/*
	void changeSprite(){
		if (moveX < 0) {
			sr.sprite = spriteMovingL;
			//direction = 2;
			direction = directionEnum.LEFT;
		} else if (moveX > 0) {
			sr.sprite = spriteMovingR;
			//direction = 0;
			direction = directionEnum.RIGHT;
		} else if (moveY > 0) {
			sr.sprite = spriteMovingU;
			//direction = 1;
			direction = directionEnum.UP;
		} else if (moveY < 0) {
			sr.sprite = spriteMovingD;
			//direction = 3;
			direction = directionEnum.DOWN;
		} else {
			switch ((int)direction) {
			case 0:
				sr.sprite = spriteIdleR;
				break;
			case 1:
				sr.sprite = spriteIdleU;
				break;
			case 2:
				sr.sprite = spriteIdleL;
				break;
			case 3:
				sr.sprite = spriteIdleD;
				break;
			}
		}
	}
	*/
}
