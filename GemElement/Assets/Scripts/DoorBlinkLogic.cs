using UnityEngine;
using System.Collections;

public class DoorBlinkLogic : MonoBehaviour {

    public AudioSource auPuzzleSolved;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        GameObject[] Switches = GameObject.FindGameObjectsWithTag("Switch");

        if(Switches[0].GetComponent<SwitchLogic>().bActive &&
            Switches[1].GetComponent<SwitchLogic>().bActive)
        {

            StartCoroutine(PlaySoundandDie());
            
        }
        
	}
    
    IEnumerator PlaySoundandDie()
    {
        auPuzzleSolved.Play();

        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);

    }


}
