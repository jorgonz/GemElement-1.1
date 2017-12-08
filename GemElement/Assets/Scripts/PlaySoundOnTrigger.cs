using UnityEngine;
using System.Collections;

public class PlaySoundOnTrigger : MonoBehaviour {


    public AudioSource auSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Player")
        {
            StartCoroutine(SongandDie());
        }
    }

    IEnumerator SongandDie()
    {
        auSound.Play();

        yield return new WaitForSeconds(6.0f);

        Destroy(this.gameObject);
    }
}
