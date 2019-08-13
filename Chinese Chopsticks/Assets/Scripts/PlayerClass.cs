using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by MATT
/// <para>
/// This is the player class
/// used to hold both hands and
/// </para>
/// a number of useful funtions
/// </summary>

public class PlayerClass
{

    /// <summary>Defining number... not needed, add name?</summary>
    private string playerID;
    /// <summary>Could use an enum since its 0/DEAD to 5/DEAD</summary>
    private int leftHand, rightHand;
    /// <summary>Current hand for action (selected/attacked)</summary>
    private char handPicked;
    /// <summary>Int used for a hand to number reffrence <para>Currently not being used</para></summary>
    private int numHandPicked;

    /// <summary>
    /// Peramatised constuctor
    /// </summary>
    /// <param name="inputID"></param>
    public PlayerClass(string inputID)
    {

        playerID = inputID;
        leftHand = 1;
        rightHand = 1;
        handPicked = 'E';

    }

    /// <summary>
    /// Default constuctor
    /// </summary>
    public PlayerClass()
    {

        playerID = "Player";
        leftHand = 1;
        rightHand = 1;
        handPicked = 'E';

    }

    /// <summary>
    /// Destructor... more or less not needed.
    /// </summary>
    ~PlayerClass()
    {

    }

    /// <summary>
    /// Attempt at a deep info copy funtion
    /// <para>
    /// Used to copy the PlayerClass objects value instead of acting like a pointer
    /// </para>
    /// </summary>
    /// <param name="pIn"></param>
    public void classInfoCopy(PlayerClass pIn)
    {

        //playerID = pIn.getPlayerID(); //this was confusing for testing lol
        leftHand = pIn.getHand('L');
        rightHand = pIn.getHand('R');
        handPicked = pIn.getHandPicked();
        numHandPicked = pIn.getNumHandPicked();

    }

    /// <summary>
    /// Funtion to display the PlayerClass info faster in Debug.Log
    /// </summary>
    public void DLog()
    {

        Debug.Log("");
        Debug.Log("Player ID: " + playerID);
        Debug.Log("Left hand: " + leftHand);
        Debug.Log("Right hand: " + rightHand);
        Debug.Log("Hand picked: " + handPicked);
        Debug.Log("Number of hand picked: " + numHandPicked);
        Debug.Log("");

    }

    /// <summary>
    /// Sets the numHand value
    /// <para>Currently not in use</para>
    /// </summary>
    /// <param name="newVal"></param>
    public void setNumHandPicked(int newVal)
    {

        numHandPicked = newVal;

    }

    /// <summary>
    /// Returns numHand value 
    /// <para>Currently not in use</para>
    /// </summary>
    /// <returns>Returns numHandPicked</returns>
    public int getNumHandPicked()
    {

        return numHandPicked;

    }

    /// <summary>
    /// Sets char for hand picked
    /// </summary>
    /// <param name="sHand"></param>
    public void setHandPicked(char sHand)
    {

        handPicked = sHand;

    }

    /// <summary>
    /// Returns char handPicked
    /// </summary>
    /// <returns>Returns char of hand picked to be attacked or attacking</returns>
    public char getHandPicked()
    {

        return handPicked;

    }

    /// <summary>
    /// A hand split, only will work if the other hand is zero, also will give the lesser number
    /// </summary>
    public void splitHand()
    {

        float tempFloat;
        if (rightHand > 1 && leftHand == 0)
        {

            tempFloat = rightHand / 2;
            leftHand = Mathf.FloorToInt(tempFloat); //gives the lesser number if remander
            //rightHand = Mathf.CeilToInt(tempFloat); //gives the greater number if remander (no it doesn't)
            rightHand -= leftHand; //this sould work

        }
        else if (leftHand > 1 && rightHand == 0)
        {

            tempFloat = leftHand / 2;
            rightHand = Mathf.FloorToInt(tempFloat);
            //leftHand = Mathf.CeilToInt(tempFloat);
            leftHand -= rightHand;

        }

    }

    /// <summary>
    /// Add to hand but with the rules of the game if you hit 5 or over you go to zero
    /// </summary>
    /// <param name="LoR"></param>
    /// <param name="newVal"></param>
    public void addToHand(char LoR, int newVal)
    {

        if (LoR == 'R')
        {

            rightHand += newVal;
            if (rightHand > 4)
                rightHand = 0;

        }
        else if (LoR == 'L')
        {

            leftHand += newVal;
            if (leftHand > 4)
                leftHand = 0;
            
        }

    }

    /// <summary>
    /// Instead of adding value this is used to set it
    /// </summary>
    /// <param name="LoR"></param>
    /// <param name="newVal"></param>
    public void setHand(char LoR, int newVal)
    {

        if (LoR == 'R')
            rightHand = newVal;
        else
            leftHand = newVal;

    }

    /// <summary>
    /// Returns the desired hands value
    /// </summary>
    /// <param name="LoR"></param>
    /// <returns>Retunrs the int for the left 'L' or right 'R' hand</returns>
    public int getHand(char LoR)
    {

        if (LoR == 'R')
            return rightHand;
        else //'L'
            return leftHand;

    }

    /// <summary>
    /// Returns player name/ID
    /// </summary>
    /// <returns>Returns string for Player name/ID</returns>
    public string getPlayerID()
    {
        return playerID;
    }

    /// <summary>
    /// Sets player name/ID
    /// </summary>
    /// <param name="inputID"></param>
    public void setPlayerID(string inputID)
    {
        playerID = inputID;
    }

}
