using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class MenuBackgroundChange : MonoBehaviour {

	private Ray ray;
	private RaycastHit hit;
	private GraphicRaycaster graphicRay;
	public RawImage background;
	public Texture noSelection, newGame, conceptArt, quit;

    public AudioSource auSound;

    private PointerEventData ped;

    public Image imgBlackFadeIn;

	public void LoadScene(string sceneToLoad){

        auSound.Play();
        imgBlackFadeIn.enabled = true;
        StartCoroutine(StartGame(sceneToLoad));
		
		Debug.Log (sceneToLoad);
	}

	public void QuitGame(){
        auSound.Play();
        StartCoroutine(Quit());
	}

	void Start(){
		graphicRay = this.GetComponent<GraphicRaycaster> ();
		ped = new PointerEventData (EventSystem.current);
	}

	void Update(){
		ped.position = Input.mousePosition;
		List<RaycastResult> results = new List<RaycastResult> ();
		EventSystem.current.RaycastAll (ped, results);
		if(results.Count == 2){
			foreach(RaycastResult rr in results){
				if(rr.gameObject.name == "newGame"){
					background.texture = newGame;
				}
				else if (rr.gameObject.name == "conceptArt"){
					background.texture = conceptArt;
				}
				else if (rr.gameObject.name == "quit"){
					background.texture = quit;
				}
			}
		}
		else{
			background.texture = noSelection;
		}

	}

    IEnumerator StartGame (string sLevel)
    {
        imgBlackFadeIn.CrossFadeAlpha(255f,10f, false);

        yield return new WaitForSeconds(3f);

        Application.LoadLevel(sLevel);
    }

    IEnumerator Quit ()
    {
        yield return new WaitForSeconds(1.0f);
        Application.Quit();
    }

}
