using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WhiteFadeIn : MonoBehaviour {


    public Image imgFadeIn;
	// Use this for initialization
	void Start () {

        StartCoroutine(Delay());
        StartCoroutine(Kill());
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        imgFadeIn.CrossFadeAlpha(0f, 10f, false);
    }

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(12f);
        imgFadeIn.enabled = false;
    }

}
