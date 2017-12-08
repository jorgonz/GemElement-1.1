using UnityEngine;
using System.Collections;

public class RingBehaviour : MonoBehaviour {

    private float fWallDuration;
    private float fGrowRate;
    private float fTTL;
    private float fTTLTimeStamp;

    // Use this for initialization
    void Start()
    {

        fTTL = 0.5f;
        fTTLTimeStamp = Time.time + fTTL;
        fWallDuration = 0.50f;
        fGrowRate = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {

        Grow();
        CheckTTL(fTTLTimeStamp);

    }

    void Grow()
    {
        this.transform.localScale += new Vector3(1, 1, 0) * fGrowRate * Time.deltaTime;
    }



    void CheckTTL(float fTimeStamp)
    {
        if (Time.time >= fTTLTimeStamp)
        {
            Destroy(this.gameObject);
        }
    }
}
