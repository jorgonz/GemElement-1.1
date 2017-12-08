using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour {

	public int playerLife;
	private int maxLife;
	public RawImage[] hearts;
	public Texture filledHeart, emptyHeart;

    public static PlayerController instance { get; set; }

    private bool bLives;

	public void decreaseLife(){
		if (playerLife >= 1) {
			hearts [maxLife - playerLife].texture = emptyHeart;
			playerLife--;
		}
	}

	void increaseLife(){
		if (playerLife < maxLife) {
			hearts [maxLife - playerLife - 1].texture = filledHeart;
			playerLife++;
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Projectile"){
			decreaseLife ();
            Destroy(other.gameObject);
		}
	}

	void fillHearts(){
		for(int i=0; i<5; i++){
			hearts [i].texture = filledHeart;
		}
	}

	// Use this for initialization
	void Start () {
		playerLife = maxLife = 5;
		hearts = new RawImage[5];
        PopulateHealthBar();
        fillHearts ();
        bLives = true;
        instance = this;


        
	}
	
	// Update is called once per frame
	void Update () {

        if(playerLife <= 0 && bLives)
        {
            GameOver();
            bLives = false;
        }
		
	}

    void GameOver()
    {
        Sprite[] sprites = GameController.instance.sprStand;
        GameController.instance.isOnDialogue = true;
        GameObject gbjPlayer = GameObject.FindGameObjectWithTag("Player");
        gbjPlayer.GetComponent<Animator>().enabled = false;
        StartCoroutine(KillPlayer(sprites,gbjPlayer));

           
    }

    IEnumerator KillPlayer(Sprite [] sprites, GameObject gbjPlayer)
    {
        for(int i = sprites.Length-1; i>=0; i--)
        {
            gbjPlayer.GetComponent<SpriteRenderer>().sprite = sprites[i];

            yield return new WaitForSeconds(1.0f);
        }


        Destroy(GameController.instance.gameObject);

        Destroy(GameObject.Find("DailogueCanvas"));

        Destroy(GameObject.Find("Canvas"));

        Application.LoadLevel("MenuOPOP");
    }

    void PopulateHealthBar()
    {
        for(int i = 0; i<5; i++)
        {
            hearts[i] = GameObject.Find("Life" + (i + 1).ToString()).GetComponent<RawImage>();
        }
    }

}
