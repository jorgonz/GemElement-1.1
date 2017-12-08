using UnityEngine;
using System.Collections;

public class StopAddForce : MonoBehaviour {

	void Start(){
		if (this.gameObject.tag == "Object")
			GetComponent<Rigidbody2D> ().isKinematic = true;
	}

	void OnCollisionStay2D(Collision2D other){
		if (!Pull.checkCollision) {
			if (this.tag == "Object" && this.gameObject == Pull.objectCollided) {
				GetComponent<Rigidbody2D> ().isKinematic = true;
                this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
				Pull.objectCollided = null;
			}
		}

		if (this.gameObject == Push.objectPushed){
			Push.objectPushed.GetComponent<Rigidbody2D> ().isKinematic = true;
            //this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            Push.objectPushed = null;
		}

		if (this.tag == "Projectile"){
			Pull.objectCollided = null;
			Destroy (this.gameObject);
		}
	}
}
