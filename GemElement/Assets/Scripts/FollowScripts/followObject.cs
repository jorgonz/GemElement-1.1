using UnityEngine;
using System.Collections;

public class followObject : MonoBehaviour {

	public GameObject object_to_follow;

	//public float this_speed;
	public float damp_time = 0.15f;
	public float max_distance = 3.0f; //Max distance that the camara can follow with the damp_time
	public float target_move_speed = 3.0f; //Speed needs to be equal to player speed
	public Vector2 map_initial_position = Vector2.zero;
	public static float map_size_x = 16.8f;
	public static float map_size_y = 14.4f;

	private Vector3 velocity = Vector3.zero;
	private Vector2 this_position;
	private Vector2 target_position;
	private Rigidbody2D this_rb;

	private float this_min_x;
	private float this_max_x;
	private float this_min_y;
	private float this_max_y;

    public static followObject instance { get; set; }

	// Update is called once per frame
	void Start(){

        instance = this;

		this_rb = GetComponent<Rigidbody2D> ();

		//http://answers.unity3d.com/questions/501893/calculating-2d-camera-bounds.html
		//(start)
		var vertExtent = (float)GetComponent<Camera>().orthographicSize;    
		var horzExtent = (float)(vertExtent * Screen.width / Screen.height);

		// Calculations assume map is position at the origin
		this_min_x = (float)(horzExtent - map_size_x / 2.0);
		this_max_x = (float)(map_size_x / 2.0 - horzExtent);
		this_min_y = (float)(vertExtent - map_size_y / 2.0);
		this_max_y = (float)(map_size_y / 2.0 - vertExtent);


		//(end)
	}

	void LateUpdate () {


		this_position = new Vector2(transform.position.x, transform.position.y);
		target_position = new Vector2(object_to_follow.transform.position.x, object_to_follow.transform.position.y);
	

		var pos = Vector2.zero;
		if (Vector2.Distance (this_position, target_position) > max_distance ) {
			Vector2 velocity = target_position - this_position;

			velocity = new Vector2 (velocity.x * target_move_speed, velocity.y * target_move_speed);
			velocity.Normalize ();

			pos = (Vector2)transform.position;
			if (pos.x <= this_min_x && velocity.x < 0 ) {
				//velocity = new Vector2 (0, velocity.y);
				velocity.Scale (new Vector2 (0, target_move_speed));
			}else if (pos.x >= this_max_x && velocity.x > 0){
				//velocity = new Vector2 (0, velocity.y);
				velocity.Scale (new Vector2 (0, target_move_speed));
			}

			if (pos.y <= this_min_y && velocity.y < 0) {
				//velocity = new Vector2 (velocity.x, 0);
				velocity.Scale (new Vector2 (target_move_speed, 0));
			} else if (pos.y >= this_max_y && velocity.y > 0) {
				//velocity = new Vector2 (velocity.x, 0);
				velocity.Scale (new Vector2 (target_move_speed, 0));
			}


			this_rb.velocity = velocity;
			pos = new Vector2(Mathf.Clamp(pos.x, this_min_x,this_max_x), Mathf.Clamp(pos.y,this_min_y,this_max_y));
			transform.position = new Vector3 (pos.x, pos.y, -10.0f);

		} else {
			this_rb.velocity = Vector2.zero;
            

			pos = Vector3.SmoothDamp(this_position, target_position, ref velocity, damp_time);
			pos = new Vector2(Mathf.Clamp(pos.x, this_min_x,this_max_x), Mathf.Clamp(pos.y,this_min_y,this_max_y));
			transform.position = new Vector3 (pos.x, pos.y, -10.0f);
		}
	}
}
