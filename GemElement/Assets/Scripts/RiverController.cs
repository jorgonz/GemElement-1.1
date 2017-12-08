using UnityEngine;
using System.Collections;

public class RiverController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        CheckforWarp();

        RotateTornado();
	}

    void CheckforWarp()
    {
        if(GemController.objectsStopped)
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<BoxCollider2D>().enabled = false;
                
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    void RotateTornado()
    {

        if(!GemController.objectsStopped)
        {
            foreach (Transform child in transform)
            {
                if(child.gameObject.tag == "WaterTornado")
                {
                    child.Rotate(new Vector3(0, 0, -1) * Time.deltaTime * 150);
                }
                
            }
        }

    }
}
