using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class conceptArtSlideshow : MonoBehaviour {

	public RawImage[] conceptArts;
	private int imageIndex = 0;

	// Use this for initialization
	void Start () {
		// conceptArts = new RawImage[4];
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return)){
			if (imageIndex == 3) {
				Application.LoadLevel ("MenuOPOP");
			} 
			else {
				conceptArts [imageIndex].CrossFadeAlpha (0f, 2f, false);
				imageIndex++;
			}
		}
	}
}
