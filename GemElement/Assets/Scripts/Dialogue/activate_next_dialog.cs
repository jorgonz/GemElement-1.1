using UnityEngine;
using System.Collections;

public class activate_next_dialog : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameController.instance.StartDailogueScene();
            Destroy(gameObject);
        }

    }

}
