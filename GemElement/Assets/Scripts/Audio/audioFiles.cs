using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class audioFiles : MonoBehaviour {

	public static Dictionary<string, AudioSource> sounds;
	private static AudioSource[] audioList;
	private bool isInGrass;
	private Rigidbody2D rbPlayer;

	// Use this for initialization
	void Start () {
		isInGrass = false;
		rbPlayer = GetComponent<Rigidbody2D> ();

		sounds = new Dictionary<string, AudioSource> ();
		audioList = this.GetComponents<AudioSource>();
		Debug.Log (audioList.Length);
		for (int i = 0; i < audioList.Length; i++) {
			sounds [audioList [i].clip.name] = audioList [i];
		}
	}

	void CheckBackgroundMusic() {
		string activeScene = SceneManager.GetActiveScene ().name;

		// Checks if the active scene is any version of the Overworld, in order to display the corresponding
		// background music.
		if (activeScene == "OverWorld" || activeScene == "OverWorld2" || activeScene == "OverWorld3") {
			// If the background music of the other scene is still playing, stop it.
			if (sounds ["Pull2_Room"].isPlaying)
				sounds ["Pull2_Room"].Stop ();

            if (sounds["Blink_Room"].isPlaying)
                sounds["Blink_Room"].Stop();

            if (sounds["Warp_Room"].isPlaying)
                sounds["Warp_Room"].Stop();

            // If the background music of this scene is not playing, let it play.
            if (!sounds ["Overworld"].isPlaying)
				sounds ["Overworld"].Play ();
		}

		// Checks if the active scene is Pull2.
		else if (activeScene == "Pull2") {
			// If the background music of the previous scene is still playing, stop it.
			if (sounds ["Overworld"].isPlaying)
				sounds ["Overworld"].Stop ();

			// If the background music of this scene is not playing, let it play.
			if (!sounds ["Pull2_Room"].isPlaying)
				sounds ["Pull2_Room"].Play ();
		}

		// Checks if the active scene is Blink1.
		else if (activeScene == "Blink1") {
			// If the background music of the previous scene is still playing, stop it.
			if (sounds ["Overworld"].isPlaying)
				sounds ["Overworld"].Stop ();

			// If the background music of this scene is not playing, let it play.
			if (!sounds ["Blink_Room"].isPlaying)
				sounds ["Blink_Room"].Play ();
		}

        else if (activeScene == "Warp1")
        {
            // If the background music of the previous scene is still playing, stop it.
            if (sounds["Overworld"].isPlaying)
                sounds["Overworld"].Stop();

            // If the background music of this scene is not playing, let it play.
            if (!sounds["Warp_Room"].isPlaying)
                sounds["Warp_Room"].Play();
        }
	}
	
	// Update is called once per frame
	void Update () {
		CheckBackgroundMusic ();
		if (rbPlayer.velocity != Vector2.zero) {
			if (isInGrass && !(sounds ["Grass"].isPlaying))
				sounds ["Grass"].Play ();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Grass")
			isInGrass = true;
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Grass")
			isInGrass = false;
	}
}
