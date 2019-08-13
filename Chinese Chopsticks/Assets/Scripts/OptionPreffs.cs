using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Made By MATT
/// <para>
/// Option Preffs
/// A simple player preffs script <br/>
/// to save and load player preffrences.
/// </para>
/// </summary>

public class OptionPreffs : MonoBehaviour
{

    /// <summary>Holder for volume slider</summary>
    public Slider volumeSlider;
    /// <summary>Value for volume</summary>
    private float volumePref;

    /// <summary>Holder for graphics drop</summary>
    public Dropdown graphicsDrop;
    /// <summary>Value for graphics</summary>
    private int graphicsValPref;

    // Start is called before the first frame update
    void Start()
    {

        volumePref = PlayerPrefs.GetFloat("Volume", 1);
        //Debug.Log("Volume Pref at " + volumePref);
        volumeSlider.value = volumePref;

        graphicsValPref = PlayerPrefs.GetInt("GraphicsVal", 5);
        graphicsDrop.value = graphicsValPref;

    }

    /// <summary>
    /// Saves volume as player preff
    /// </summary>
    public void setVolume()
    {

        volumePref = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volumePref);

    }

    /// <summary>
    /// Saves volume as player preff
    /// </summary>
    public void setGraphicsVal()
    {

        graphicsValPref = graphicsDrop.value;
        PlayerPrefs.SetInt("GraphicsVal", graphicsValPref);

    }

    /// <summary>
    /// Saves player 1 name/ID as player preff
    /// </summary>
    /// <param name="newName"></param>
    public void setPlayer1Name(string newName)
    {

        PlayerPrefs.SetString("Player1Name", newName);

    }

    /// <summary>
    /// Saves player 2 name/ID as player preff
    /// </summary>
    /// <param name="newName"></param>
    public void setPlayer2Name(string newName)
    {

        PlayerPrefs.SetString("Player2Name", newName);

    }

}
