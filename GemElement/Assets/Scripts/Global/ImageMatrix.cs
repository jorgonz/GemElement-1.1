using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// This is a little trick to mimick a Matrix of Sprites but still have it
/// show in the Unity editor, we must have an Array of Arrays of Sprites instead
/// of a matrix, but it works very similar.
/// </summary>
[System.Serializable]
public class imageMatrix
{

    public Sprite[] arrImage;

}
