using UnityEngine;
using System.Collections;

public class movePanel : MonoBehaviour {

	// 0 for up-down, 1 for left-right
	public int direction;

	// Time that panel moves towards direction
	public float time;

	// Time when panel changed direction
	private float lastDirectionChange;

	private Vector3 currentDirection;
	public float moveSpeed;

	// Use this for initialization
	void Start () {

        /*if (this.gameObject == Level_Pull2.Panel1)
            this.transform.position = new Vector3(-5.11f, -3.447f, 0f);
        else if (this.gameObject == Level_Pull2.Panel2)
            this.transform.position = new Vector3(5.23f, -3.447f, 0f);
        else if (this.gameObject == Level_Pull2.Panel3)
            this.transform.position = new Vector3(-5.04f, 4.09f, 0f);*/

        lastDirectionChange = 0;
		if (direction == 1){
			currentDirection = Vector3.right * moveSpeed;
		}
		else if(direction == 0){
			currentDirection = Vector3.up * moveSpeed;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > (lastDirectionChange + time)){
			lastDirectionChange = Time.time;
			currentDirection = -1 * currentDirection;
		}

		this.transform.Translate (currentDirection);
	}
}
