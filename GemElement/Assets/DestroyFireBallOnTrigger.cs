using UnityEngine;
using System.Collections;

public class DestroyFireBallOnTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
