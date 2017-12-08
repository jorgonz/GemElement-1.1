using UnityEngine;
using System.Collections;

public class SwitchLogic : MonoBehaviour {

    public bool bActive;

	// Use this for initialization
	void Start () {

        bActive = false;
	
	}
	
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.transform.tag == "Object")
        {
            bActive = true;
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.transform.tag == "Object")
        {
            bActive = false;
        }
        
    }
}
