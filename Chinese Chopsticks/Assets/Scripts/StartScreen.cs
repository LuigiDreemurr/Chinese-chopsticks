using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by MATT
/// <para>
/// Simple script for the menu/start screen
/// </para>
/// <para>
/// To avoid having the P2 be AI after a game
/// ...Maybe I should just add an if and avoid this
/// </para>
/// so it can save P1 name...
/// </summary>

public class StartScreen : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

        PlayerPrefs.SetString("Player1Name", "Player 1"); //Loads default names
        PlayerPrefs.SetString("Player2Name", "Player 2");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
