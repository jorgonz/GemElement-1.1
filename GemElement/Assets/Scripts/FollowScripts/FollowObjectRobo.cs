using UnityEngine;
using System.Collections;

public class FollowObjectRobo : MonoBehaviour {

    public GameObject object_to_follow;
    //public float this_speed;
    public float damp_time = 0.15f;
    public float max_distance = 3.0f; //Max distance that the camara can follow with the damp_time
    public float target_move_speed = 3.0f; //Speed needs to be equal to player speed

    private Vector2 velocity = Vector2.zero;
    private Vector2 this_position;
    private Vector2 target_position;
    private Rigidbody2D this_rb;
    private Rigidbody2D target_rb;
    // Update is called once per frame
    void Start()
    {
        this_rb = GetComponent<Rigidbody2D>();
        target_rb = object_to_follow.GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {


        this_position = new Vector2(transform.position.x, transform.position.y);
        target_position = new Vector2(object_to_follow.transform.position.x, object_to_follow.transform.position.y);

        if (Vector2.Distance(this_position, target_position) > max_distance)
        {
            Vector2 velocity = target_position - this_position;

            velocity = new Vector2(velocity.x * target_move_speed, velocity.y * target_move_speed);
            velocity.Normalize();
            velocity.Scale(new Vector2(target_move_speed, target_move_speed));
            this_rb.velocity = velocity;
        }
        else
        {
            this_rb.velocity = Vector2.zero;
            Vector3 vc3Instance = new Vector3(velocity.x, velocity.y, 0);
            transform.position = Vector3.SmoothDamp(this_position, target_position, ref vc3Instance , damp_time);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
        }
    }
}
