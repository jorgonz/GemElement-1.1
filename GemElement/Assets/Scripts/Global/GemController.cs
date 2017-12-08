using UnityEngine;
using System.Collections;

public class GemController : MonoBehaviour {
	public GameObject select_gem_UI;
	public enum ActiveGem {PULL_PUSH, BLINK, WARP};
	public static ActiveGem gem;
	public static bool objectsStopped;
	public static bool blinkObtained, warpObtained;

	//private Animator animator_selector_gems;
	// Use this for initialization
	void Start () {
		gem = ActiveGem.PULL_PUSH;
		select_gem_UI.GetComponent<Animator> ().SetInteger ("gem_using", 1);
		select_gem_UI.GetComponent<Animator> ().SetInteger ("gems_amount", 1);
		blinkObtained = warpObtained = true;
		objectsStopped = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (warpObtained) {
			select_gem_UI.GetComponent<Animator> ().SetInteger ("gems_amount", 3);
		} else if (blinkObtained) {
			select_gem_UI.GetComponent<Animator> ().SetInteger ("gems_amount", 2);
		}else
			select_gem_UI.GetComponent<Animator> ().SetInteger ("gems_amount", 1);

		if ((Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt) || Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))) {
			if (!Pull.hasObjectRotating) {
				if (gem == ActiveGem.PULL_PUSH && blinkObtained) {
					gem = ActiveGem.BLINK;
					select_gem_UI.GetComponent<Animator> ().SetInteger ("gem_using", 1);
				} else if (gem == ActiveGem.BLINK && warpObtained) {
					gem = ActiveGem.WARP;
					select_gem_UI.GetComponent<Animator> ().SetInteger ("gem_using", 2);
				} else if (gem == ActiveGem.WARP) {
					gem = ActiveGem.PULL_PUSH;
					select_gem_UI.GetComponent<Animator> ().SetInteger ("gem_using", 0);
				}
				select_gem_UI.GetComponent<Animator> ().SetTrigger ("change_gem");
				Debug.Log (gem.ToString ());
			}
		}
	}
}
