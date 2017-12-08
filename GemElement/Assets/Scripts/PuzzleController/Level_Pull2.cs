using UnityEngine;
using System.Collections;

public class Level_Pull2 : MonoBehaviour {

	public static GameObject Panel1, Panel2, Panel3;
	public static bool hitPanel1, hitPanel2, hitPanel3;
	public static int panelsHitted;
	public float panelCountDown;
	public static float firstPanelHit;

    public AudioSource auGemObtained;

	public GameObject blinkGem, totem;
	private Vector2 gemEndPoint;

	public Sprite panelOff;

	private bool canPlaySound;

	// Use this for initialization
	void Start () {
		totem = GameObject.Find ("Totem");
		Panel1 = GameObject.Find ("Panel UD_Left");
		Panel2 = GameObject.Find ("Panel UD_Right");
		Panel3 = GameObject.Find ("Panel LR");
		hitPanel1 = hitPanel2 = hitPanel3 = false;
		panelsHitted = 0;
		gemEndPoint = new Vector2 (0.2f, 0f);
		canPlaySound = true;
	}
	
	// Update is called once per frame
	void Update () {
		// Restart panels when time runs out

		if (Time.time > (firstPanelHit + panelCountDown) && panelsHitted != 3){
			hitPanel1 = hitPanel2 = hitPanel3 = false;

			Panel1.GetComponent<SpriteRenderer> ().sprite = panelOff;
			Panel2.GetComponent<SpriteRenderer> ().sprite = panelOff;
			Panel3.GetComponent<SpriteRenderer> ().sprite = panelOff;
			panelsHitted = 0;
		}

		if (panelsHitted == 3){
			if (/*!audioFiles.sounds ["puzzle solved"].isPlaying &&*/ canPlaySound) {
                //audioFiles.sounds ["puzzle solved"].Play ();
                auGemObtained.Play();
				canPlaySound = false;
			}

			Panel1.SetActive (false);
			Panel2.SetActive (false);
			Panel3.SetActive (false);
			totem.SetActive (false);

			if (blinkGem != null) {
				blinkGem.SetActive (true);
				blinkGem.transform.Translate (0.01f * (gemEndPoint - (Vector2)blinkGem.transform.position));
			}

			 
		}

	}
}
