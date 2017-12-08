using UnityEngine;
using System.Collections;

public class Level_Warp1 : MonoBehaviour {

	public static GameObject Panel1_W1, Panel2_W1;
	public static bool hitPanel1_W1, hitPanel2_W1;
	public static int panelsHitted_W1;
	public float panelCountDown;
	public static float firstPanelHit_W1;

	public GameObject totem;
	public GameObject boxGen1, boxGen2, boxGen3;

	public Sprite panelOff;

	// Use this for initialization
	void Start () {
		totem = GameObject.Find ("Totem");
		boxGen1 = GameObject.Find ("BlockSpawner1");
		boxGen2 = GameObject.Find ("BlockSpawner2");
		boxGen3 = GameObject.Find ("BlockSpawner3");
		Panel1_W1 = GameObject.Find ("Panel L");
		Panel2_W1 = GameObject.Find ("Panel R");

		hitPanel1_W1 = hitPanel2_W1 = false;
		panelsHitted_W1 = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > (firstPanelHit_W1 + panelCountDown) && panelsHitted_W1 != 2){
			hitPanel1_W1 = hitPanel2_W1 = false;

			Panel1_W1.GetComponent<SpriteRenderer> ().sprite = panelOff;
			Panel2_W1.GetComponent<SpriteRenderer> ().sprite = panelOff;

			panelsHitted_W1 = 0;
		}

		if (panelsHitted_W1 == 2){
			Panel1_W1.SetActive (false);
			Panel2_W1.SetActive (false);
			boxGen1.SetActive (false);
			boxGen2.SetActive (false);
			boxGen3.SetActive (false);
			totem.SetActive (false);
		}
	}
}
