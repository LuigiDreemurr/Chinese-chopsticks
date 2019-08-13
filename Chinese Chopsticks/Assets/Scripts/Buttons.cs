using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

/// <summary>
/// Made By MATT
/// <para>Buttons Script
/// Mostly used in options but has a bool <br/>
/// so it can be used other places as well.</para>
/// </summary>

public class Buttons : MonoBehaviour
{

    /// <summary>Bool for if this script has an options screen</summary>
    public bool isOptions;
    /// <summary>Holder for options screen</summary>
    public GameObject optionsScreen;
    /// <summary>Holder for volume slider</summary>
    public Slider volumeSlider;
    /// <summary>Holder for graphics drop</summary>
    public Dropdown graphicsDrop;
    /// <summary>Holder for rules screen</summary>
    public GameObject rulesScreen;

    private void Start()
    {

        

    }

    /// <summary>
    /// Togel rule screen
    /// </summary>
    public void ruleScreenFun()
    {

        rulesScreen.SetActive(!rulesScreen.active);

    }

    /// <summary>
    /// Togel options screen
    /// </summary>
    public void optionsScreenFun()
    {

        optionsScreen.SetActive(!optionsScreen.active); //What isn't obsolete nowadays

    }

    private void Update()
    {

        if (isOptions)
        {

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {

                optionsScreenFun();

            }

        }
        
    }

    /// <summary>
    /// Sets screen quality
    /// </summary>
    /// <param name="newValue"></param>
    public void SetQuality(int newValue)
    {

        QualitySettings.SetQualityLevel(newValue);

    }

    /// <summary>
    /// Sets volume
    /// </summary>
    public void volumeControl()
    {

        if ((AudioSource)FindObjectOfType(typeof(AudioSource)) == null)
            return;

        AudioSource sourceA = (AudioSource)FindObjectOfType(typeof(AudioSource));

        sourceA.volume = volumeSlider.value;

    }

    /// <summary>
    /// New version can load any scene using the built in number system
    /// </summary>
    /// <param name="sceneNum"></param>
    public void sceneButton(int sceneNum)
    {

        SceneManager.LoadScene(sceneNum);

    }

    /// <summary>
    /// Application quit
    /// </summary>
    public void exitGameButton()
    {

        Application.Quit();

    }

    /// <summary>
    /// Turns off Options screen
    /// </summary>
    public void resumeButton()
    {

        Time.timeScale = 1;
        //if (isPaused != exists)
        optionsScreen.SetActive(false);
        //Debug.Log("Resume is clicked");

    }

}
