using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// GameController is a very big Script, it handles all the background info
/// as well as the most of the dialogue code, most of the information game logic
/// is done here
/// </summary>
public class GameController : MonoBehaviour {

    #region Declarations

    //Instance of this script to ease acess in other scripts
    public static GameController instance { get; set; }

    //Here we Store the Settings for the x and y size of the camera in each
    //scene
    public Pair[] CameraSizeSettings;

    //OverWorld Status, we use this number to know what cutscenes to play
    public int iOverWorldStatus;

    //Flag that tells if the player is in a dialogue or not.
    public bool isOnDialogue;

    //An Array of TextFiles with the info of the Dialogues
    public TextAsset[] textDialogues;

    //The number of the next dialogue to play
    private int iDialogueIndex;

    //The number of dialoges in the game
    private int iCountDialogues;

    //A reference to the gameobject that controls the dialogues
    public GameObject gbjTextBoxManager;

    //Reference to the canvas that holds the dialogue objects
    public Canvas cvsDialogueCanvas;

    //Reference to the textbox inside the canvas of dialogues
    public GameObject gbjTextContainer;

    //Reference to the text inside the TextContainer
    public Text txtDialogue;

    //Reference to the Array of Arrays of Sprite Images for Actor1 and Actor2 in the
    //Dialogue Scene 

    public imageMatrix[] matActor2;
    public imageMatrix[] matActor1;

    //Reference to the Images in the canvas that will show actor 1 and actor 2
    //sprites
    public Image Actor1;
    public Image Actor2;

    //Reference to the FadeIn Black Square in the Initial CutScene
    public Image imgBlackFadeIn;

    //Reference to the Sprites Used in the Initial CutScene
    public Sprite[] sprStand;

    //Reference to the Sprites Used in the CutScene that opens the door to the
    //final boss
    public Sprite[] sprBossDoor;

    //Reference to the Blue blocks used in the game
    public GameObject gbjBlock;

    //Reference to Calderito
    public GameObject gbjCalderito;

    //Reference to Totem
    public GameObject gbjTotem;

    public AudioSource auStone;

    #endregion

    // Use this for initialization
    void Start()
    {
        #region DialogueInfoInit

        //Set the instance of a GameControllerScript, to this Script
        instance = this;

        //The dialogue index starts in the first dialogue
        iDialogueIndex = 0;

        //This is the amount of dialogue in the game
        iCountDialogues = textDialogues.Length;

        //This is the the function to call for the next cutscene
        //if you wish to start the next cutscene in another script
        //write GameController.instance.StartCutScene();
        //StartDailogueScene();

        #endregion

        #region CameraBounds

        CameraSizeSettings = new Pair[5];

        Pair AuxPair = new Pair();

        CameraSizeSettings[0] = AuxPair;
        CameraSizeSettings[1] = AuxPair;
        CameraSizeSettings[2] = AuxPair;
        CameraSizeSettings[3] = AuxPair;
        CameraSizeSettings[4] = AuxPair;

        #endregion

        //Set the Initial Status of the OverWorld
        iOverWorldStatus = 0;
    }

    #region DialogueCode

    /// <summary>
    /// This function initializes and creates all the necessary information to
    /// display the next dailogue, including setting up the DialogBox, the sprites
    /// to show in the next scene, and the correct sprite transitions.
    /// </summary>
    public void StartDailogueScene()
    {
        //Validate that a next dailogue exists, or that this is the last dailogue
        if (iDialogueIndex < iCountDialogues)
        {
            //If the index is valid, start the scene, set the dialogue flag
            //to true
            isOnDialogue = true;

            //Create a TextBoxController instance, this object will control
            //the Dialogue scene, we just need to feed it info.
            GameObject gbjTextBoxInstance = (GameObject)Instantiate(gbjTextBoxManager, Vector3.zero, Quaternion.identity);

            //Make the instance son of the canvas, for order.
            gbjTextBoxInstance.transform.SetParent(cvsDialogueCanvas.transform);

            //Feed the TextBoxInstance the necessary parameters so that it can
            //work out the dialogue
            gbjTextBoxInstance.GetComponent<TextBoxManager>().gbjTextBox = gbjTextContainer;
            gbjTextBoxInstance.GetComponent<TextBoxManager>().arrImageAct1 = matActor1[iDialogueIndex].arrImage;
            gbjTextBoxInstance.GetComponent<TextBoxManager>().arrImageAct2 = matActor2[iDialogueIndex].arrImage;
            gbjTextBoxInstance.GetComponent<TextBoxManager>().textFile = textDialogues[iDialogueIndex++];
            gbjTextBoxInstance.GetComponent<TextBoxManager>().txtDialogue = txtDialogue;
            gbjTextBoxInstance.GetComponent<TextBoxManager>().imgActor1 = Actor1;
            gbjTextBoxInstance.GetComponent<TextBoxManager>().imgActor2 = Actor2;

            //Make the 3 objects that compose a dialogue in the Unity Scene to
            //Active, the Dailogue box and the two Actors of the scene.
            gbjTextBoxInstance.GetComponent<TextBoxManager>().gbjTextBox.SetActive(true);
            Actor1.gameObject.SetActive(true);
            Actor2.gameObject.SetActive(true);

            //The TextBoxManager will do the rest

        }
        else
        {
            //If the are no more scenes display an error
            Debug.LogError("They are no more Dialogues to play!");
        }


    }

    /// <summary>
    /// This Hides the Unity objects of a dialogue and turns off the dailogue
    /// flag
    /// </summary>
    public void EndCutScene()
    {
        Actor1.gameObject.SetActive(false);
        Actor2.gameObject.SetActive(false);
        isOnDialogue = false;
    }

    #endregion

    #region CutScenes

    void SimpleCutScene(GameObject gbjTarget, float fWaitTime)
    {
        isOnDialogue = true;

        StartCoroutine(SimpCutScene(gbjTarget, fWaitTime));
    }

    IEnumerator SimpCutScene(GameObject gbjTarget, float fWaitTime)
    {
        Camera.main.GetComponent<followObject>().object_to_follow = gbjTarget;

        yield return new WaitForSeconds(fWaitTime);

        Camera.main.GetComponent<followObject>().object_to_follow = GameObject.FindGameObjectWithTag("Player");

        isOnDialogue = false;
    }

    /// <summary>
    /// This are the events that ocurr at the beggining of the game.
    /// </summary>
    void InitialCutScene()
    {
        //Fade in, dissapearing the black screen.
        imgBlackFadeIn.CrossFadeAlpha(0.0f, 7.0f, false);

        //Locate and store the player gameobject
        GameObject gbjPlayer = GameObject.FindGameObjectWithTag("Player");

        //Disable it's animator
        gbjPlayer.GetComponent<Animator>().enabled = false;

        //Set it's sprite to fallen
        gbjPlayer.GetComponent<SpriteRenderer>().sprite = sprStand[0];

        //Start the Coroutine that makes the player stand up
        StartCoroutine(SlowSequenceSprites(sprStand,gbjPlayer)); 

        //Start the first Dialogue
        StartDailogueScene();
    }

    public IEnumerator SlowSequenceSprites(Sprite[] sprites, GameObject gbjTarget)
    {
        for(int i = 0; i<sprites.Length; i++)
        {
            gbjTarget.GetComponent<SpriteRenderer>().sprite = sprites[i];

            yield return new WaitForSeconds(1.5f);

            if(gbjTarget.tag == "BossDoor")
            {
                auStone.Play();
            }
        }

        try
        {
            gbjTarget.GetComponent<Animator>().enabled = true;
        }
        catch
        { }
        
    }

    void OpenBossDoor ()
    {
        GameObject gbjBossDoor = GameObject.FindGameObjectWithTag("BossDoor");

        SimpleCutScene(gbjBossDoor, 20.0f);

        StartCoroutine(SlowSequenceSprites(sprBossDoor, gbjBossDoor));

        gbjBossDoor.GetComponent<BoxCollider2D>().enabled = false;
    }

    #endregion

    void OnLevelWasLoaded()
    {
        //Every time a level loads, set the camera bounds for the new scene
        SetCameraBounds(Application.loadedLevel);
            
        if(Application.loadedLevel ==5)
        {
            followObject.map_size_x = 45f;
            followObject.map_size_y = 60f;
            Camera.main.orthographicSize = 4;
        }
        else if(Application.loadedLevel ==7)
        {
            followObject.map_size_x = 30f;
            followObject.map_size_y = 25f;
            Camera.main.orthographicSize = 5;
        }
        else
        {
            followObject.map_size_x = 16.8f;
            followObject.map_size_y = 14.4f;
            Camera.main.orthographicSize = 3;
        }

        //If the OverWorld was loaded
        if (Application.loadedLevel == 1)
        {
            //Check the Current Progress in OverWorld and Act Acordingly
            switch(iOverWorldStatus)
            {
                //FirstTime in OverWorld
                case 0:
                    FirstTimeOverWorld();
                    break;
                //Coming Back to OverWorld after beating Pull/Push
                case 1:
                    Instantiate(gbjTotem, new Vector3(-7.2f, -3.7f, 0), Quaternion.identity);
                    StartDailogueScene();
                    break;
                //Coming Back to OverWorld after beating Blink
                case 2:
                    GameObject gbj = (GameObject) Instantiate(gbjCalderito, new Vector3(-0.3f, 3, 0), Quaternion.identity);
                    gbj.GetComponent<Calderito>().player = GameObject.FindGameObjectWithTag("Player");
                    break;
                //Coming Back to OverWorld after beating Warp
                case 3:
                    ThirdTimeOverWorld();
                    break;

                default:
                    Debug.LogError("Something went wrong with Overworld Status");
                    break;
            }

            

            //Every Time you come back to OverWorld increase the status level
            //for the next time you come back.
            iOverWorldStatus++;
        }

        if (Application.loadedLevel == 1)
        {
            StartDailogueScene();
        }
    }

    void SetCameraBounds(int iLevel)
    {

        //followObject.map_size_x = CameraSizeSettings[iLevel].fX;
        //followObject.map_size_y = CameraSizeSettings[iLevel].fY;
        
        
    }

    void FirstTimeOverWorld()
    {
        //Instantiate the blue blocks, to open the door to the first puzzle
        Instantiate(gbjBlock, new Vector3(-2.2f, 2.7f, 0), Quaternion.identity);
        Instantiate(gbjBlock, new Vector3(-5.2f, -3.4f, 0), Quaternion.identity);

        //Play the First CutScene
        //InitialCutScene();
    }

    void ThirdTimeOverWorld()
    {
        OpenBossDoor();
        StartDailogueScene();
    }

}
