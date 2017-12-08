using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

    //This is a reference to the canvas' TextBox and text, we modify this
    //to show the actual dialogue in the unity scene
    public GameObject gbjTextBox;
    public Text txtDialogue;

    //This is a reference to the textfile that has the actual dialogue
    //we will split that info into an array of lines.
    public TextAsset textFile;
    private string[] arrTextLines;

    //The last line of dialogue and the current one
    public int iLastLine;
    public int iCurrentLine;

    //This is an array of sprites for Actor1 and Actor2 of the dialogue, with
    //their corresponding indexes to iterate through their different sprites
    public Sprite[] arrImageAct1;
    public Sprite[] arrImageAct2;
    private int iActor1Index;
    private int iActor2Index;

    //This is Unity Scene reference, that refereces the Sprites to show in the
    //unity canvas.
    public Image imgActor1;
    public Image imgActor2;

    //This are the flags to handle text Scrolling
    public bool isScrolling;
    public bool bCancelScroll;

    //Float Scroll Speed
    private float fScrollSpeed;

    // Use this for initialization
    void Start()
    {
        //Set Scroll Speed
        fScrollSpeed = 0.05f;

        //Initiliaze the scrolling flags
        isScrolling = false;
        bCancelScroll = false;

        //Initialize the information
        Debug.Log("Dialoge has started");

        //Store in the texfile info splited by endofline characters.
        arrTextLines = (textFile.text.Split('\n'));

        //Set the last line of dialogue
        iLastLine = arrTextLines.Length - 1;

        //Initialize the indexes
        iCurrentLine = 0;
        iActor1Index = 0;
        iActor2Index = 0;

        //Set the initial Sprites
        imgActor1.transform.GetComponent<UnityEngine.UI.Image>().sprite = arrImageAct1[0];
        imgActor2.transform.GetComponent<UnityEngine.UI.Image>().sprite = arrImageAct2[0];

        //Read the first line automatically
        ReadLine();

    }

    /// <summary>
    /// This function reads the info in the textfile and acts acordingly
    /// </summary>
    void ReadLine()
    {
        //If the first char of the line is 0 it means change no sprites
        if (arrTextLines[iCurrentLine][0] == '0')
        {
            //Remove the first char of the string
            string AuxString = arrTextLines[iCurrentLine++].Remove(0, 1);

            //Display the dialogue in the unity scene
            StartCoroutine(ScrollText(AuxString));
        }
        //If the first char of the line is 1 it means go the next sprite Actor1
        else if (arrTextLines[iCurrentLine][0] == '1')
        {
            //Increase and Set the new Sprite for Actor1
            try
            {
                imgActor1.transform.GetComponent<UnityEngine.UI.Image>().sprite = arrImageAct1[++iActor1Index];
            }
            catch
            {
                //If Something goes wrong, don't change the sprites
                Debug.LogError("Actor1 array out of bounds, check the setup for the sprites in your scene");
            }

            //Remove the first char of the string
            string AuxString = arrTextLines[iCurrentLine++].Remove(0, 1);

            //Display the dialogue in the unity scene
            StartCoroutine(ScrollText(AuxString));
        }
        //If the first char of the line is 2 it means go the next sprite Actor2
        else if (arrTextLines[iCurrentLine][0] == '2')
        {
            //Increase and Set the new Sprite for Actor2
            try
            {
                imgActor2.transform.GetComponent<UnityEngine.UI.Image>().sprite = arrImageAct2[++iActor2Index];
            }
            catch
            {
                //If Something goes wrong, don't change the sprites
                Debug.LogError("Actor2 array out of bounds, check the setup for the sprites in your scene");
            }

            //Remove the first char of the string
            string AuxString = arrTextLines[iCurrentLine++].Remove(0, 1);

            //Display the dialogue in the unity scene
            StartCoroutine(ScrollText(AuxString));
        }

    }

    // Update is called once per frame
    void Update()
    {
        //When the user presses enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //If the Dialogue isn't over
            if (iCurrentLine <= iLastLine)
            {
                if (!isScrolling)
                {
                    //If the text wasn't scrolling, go to the next line
                    ReadLine();
                }
                else
                {
                    //If the text was scrolling, cancel said scrolling.
                    bCancelScroll = true;
                }

            }
            else
            {
                if (!isScrolling)
                {
                    //If the Dialogue is over
                    Debug.Log("Dialoge has ended");

                    //Hide the dialogue box
                    gbjTextBox.SetActive(false);

                    //Hide the Actors
                    GameController.instance.EndCutScene();

                    //Destroy this TextManager, we don't need it anymore
                    Destroy(this.transform.gameObject);
                }
                else
                {
                    bCancelScroll = true;
                }

            }
        }
    }

    IEnumerator ScrollText(string sLine)
    {
        //Index for the current letter to scroll and show
        int iLetterIndex = 0;

        //Erase the previous text
        txtDialogue.text = "";

        isScrolling = true;
        bCancelScroll = false;
        while (!bCancelScroll && (iLetterIndex < sLine.Length))
        {
            txtDialogue.text += sLine[iLetterIndex++];

            yield return new WaitForSeconds(fScrollSpeed);
        }
        txtDialogue.text = sLine;
        isScrolling = false;
        bCancelScroll = false;

    }
}
