using UnityEngine;
using System.Collections;

public class mariposaAnimation : MonoBehaviour {



    public Sprite[] sprMariposa;
    float TimeStamp;
    float TimeBetweenSprites = .10f;
    int index;
	// Use this for initialization
	void Start () {

        TimeStamp = Time.time;
        index = 0;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time >= TimeStamp + TimeBetweenSprites) 
        {
            this.transform.GetComponent<SpriteRenderer>().sprite = sprMariposa[++index % sprMariposa.Length];
            TimeStamp = Time.time;
        }

       

    }

}
