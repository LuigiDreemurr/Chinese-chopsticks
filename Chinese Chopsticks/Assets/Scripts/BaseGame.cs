using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Made By MATT
/// <para>
/// Base script to test and play with
/// some game mechanics
/// </para>
/// --------------------------------------------
/// <para>
/// Has since become a funtional script
/// with the full PvP game. 
/// </para>
/// Working on a second script for PvC
/// </summary>

public class BaseGame : MonoBehaviour
{

    /// <summary>Bool to seperate script from PvP and PvC</summary>
    public bool isAI;

    /// <summary>Player1 holder</summary>
    public PlayerClass player1 = new PlayerClass("Player 1");
    /// <summary>Player2 holder</summary>
    public PlayerClass player2 = new PlayerClass("Player 2");

    /// <summary>Bool for current player turn (0 is p1 and 1 is p2)</summary>
    private bool currentTurn;
    /// <summary>Bool for GameOver (if game IS over then true)</summary>
    private bool gameOver;

    /// <summary>Holder for gameover screen overlay</summary>
    public GameObject gameOverHolder;
    /// <summary>Header text feild</summary>
    public Text headerText;
    /// <summary>Player1 text feilds</summary>
    public Text[] Player1Texts;
    /// <summary>Player2 text feilds</summary>
    public Text[] Player2Texts;

    /// <summary>
    /// Updates the player displays for player turn and hand values
    /// </summary>
    void setPlayerHands()
    {

        Player1Texts[1].text = player1.getHand('R').ToString();
        Player1Texts[2].text = player1.getHand('L').ToString();

        Player2Texts[1].text = player2.getHand('R').ToString();
        Player2Texts[2].text = player2.getHand('L').ToString();

        string mesg;
        if (!currentTurn)
            mesg = player1.getPlayerID();
        else
            mesg = player2.getPlayerID();

        mesg += "'s turn";

        headerText.text = mesg;

    }

    /// <summary>
    /// Shows the player names with ingame text
    /// </summary>
    void setNames()
    {
        
        Player1Texts[0].text = PlayerPrefs.GetString("Player1Name", player1.getPlayerID());
        Player2Texts[0].text = PlayerPrefs.GetString("Player2Name", player2.getPlayerID());

    }

    void StartGame() //could have just used Start but meh
    {

        currentTurn = false;
        gameOver = false;

        gameOverHolder.SetActive(gameOver);

        player1.setHand('R', 1);
        player1.setHand('L', 1);

        player2.setHand('R', 1);
        player2.setHand('L', 1);

        player1.setHandPicked('E');
        player2.setHandPicked('E');

        setPlayerHands();
        
        setNames();

    }

    /// <summary>
    /// button for all hands, depending on turn will pick action.
    /// </summary>
    /// <param name="handNum"></param>
    public void handButton(int handNum)
    {

        if (!gameOver)
        {

            if (handNum == 1 && player1.getHand('R') != 0)
            {
                player1.setHandPicked('R');
            }
            else if (handNum == 2 && player1.getHand('L') != 0)
            {
                player1.setHandPicked('L');
            }
            else if (handNum == 3 && player2.getHand('R') != 0)
            {
                player2.setHandPicked('R');
            }
            else if (handNum == 4 && player2.getHand('L') != 0)
            {
                player2.setHandPicked('L');
            }

            handChoice();

        }

    }

    /// <summary>
    /// Checks to see if both hands have been selected else do nothing
    /// </summary>
    void handChoice()
    {

        if (player1.getHandPicked() != 'E' && player2.getHandPicked() != 'E')
            handAttack();

    }

    /// <summary>
    /// If hand of other player then attack that hand
    /// </summary>
    void handAttack()
    {

        if (!currentTurn) //Player 1
        {
            player2.addToHand(player2.getHandPicked(), player1.getHand(player1.getHandPicked()));
        }
        else if (currentTurn) //player 2
        {
            player1.addToHand(player1.getHandPicked(), player2.getHand(player2.getHandPicked()));
        }

        player1.setHandPicked('E');
        player2.setHandPicked('E');

        swapTurn();
        setPlayerHands();

        checkForWin();

        //Debug.Log(currentTurn);

    }

    /// <summary>
    /// If either side has 00 then stops the game and displays win screen
    /// </summary>
    private void checkForWin()
    {

        if (player1.getHand('R') == 0 && player1.getHand('L') == 0)
        {
            //winner
            gameOver = true;
            gameOverHolder.SetActive(gameOver);
            headerText.text = player2.getPlayerID() + " has won the game!";

        }
        else if (player2.getHand('R') == 0 && player2.getHand('L') == 0)
        {
            //winner
            gameOver = true;
            gameOverHolder.SetActive(gameOver);
            headerText.text = player1.getPlayerID() + " has won the game!";

        }

    }

    /// <summary>
    /// A simple funtion to reset the game
    /// </summary>
    public void rematchButton()
    {

        //Invoke("StartGame", 1);
        StartGame();

    }

    /// <summary>
    /// Button funtion to split p's hand, built in rule guards
    /// </summary>
    /// <param name="pHand"></param>
    public void pSplit(bool pHand)
    {

        if (!gameOver)
        {

            if (!pHand && !currentTurn && (player1.getHand('R') == 0 || player1.getHand('L') == 0))
            {
                player1.splitHand();
                swapTurn();
                setPlayerHands();
            }
            else if (pHand && currentTurn && (player2.getHand('R') == 0 || player2.getHand('L') == 0))
            {
                player2.splitHand();
                swapTurn();
                setPlayerHands();
            }

        }
        
    }

    /// <summary>
    /// Swaps the current turn
    /// </summary>
    private void swapTurn()
    {
        currentTurn = !currentTurn;
    }

    // Start is called before the first frame update
    void Start()
    {

        if (isAI)
        {

            PlayerPrefs.SetString("Player2Name", "AI");

        }

        //Invoke("StartGame", 0.1f); //Invoke is an easy way to delay a funtion without using Ienumerator
        StartGame();

        /* //floor and ceil test stuff. Not working with varuables? More in PlayerClass
        Debug.Log(Mathf.FloorToInt(3/2));
        Debug.Log(Mathf.CeilToInt(3 / 2));
        Debug.Log(Mathf.FloorToInt(2.5f));
        Debug.Log(Mathf.CeilToInt(2.5f));
        */
        /*
        PlayerClass temp = new PlayerClass("Temp");
        PlayerClass test = new PlayerClass("Testing");

        temp.setHand('L', 2);

        temp.DLog();
        test.DLog();


        //test = temp;
        temp.setHand('L', 3);

        temp.DLog();
        test.DLog();

        test.classInfoCopy(temp);
        temp.setHand('L', 4);

        temp.DLog();
        test.DLog();
        */

    }

    void OnGUI() //Could do GUI instead.
    {

        //if (GUI.Button(new Rect(10, 10, 50, 50), "Split"))
        //    Debug.Log("Clicked the button");

    }

    // Update is called once per frame
    void Update()
    {
        
        if (isAI && getTurn() && !getGameOver()) //working on since it doesnt seem to like logic in AI?
        {
            
            AI temp = new AI();
            temp.AIDoAll(player1, player2);

            if (temp.aiTree[1].p2Holder.getHandPicked() == 'S')
            {

                pSplit(true);

            }
            else
            {

                player1.setHandPicked(temp.aiTree[1].p1Holder.getHandPicked());
                player2.setHandPicked(temp.aiTree[1].p2Holder.getHandPicked());
                
                handAttack();

            }
            
        }

    }

    /// <summary>
    /// Returns if the game has ended
    /// </summary>
    /// <returns>Returns bool for gameOver</returns>
    public bool getGameOver()
    {

        return gameOver;

    }

    /// <summary>
    /// Returns the current turn
    /// </summary>
    /// <returns>Returns bool for turn value <see cref ="currentTurn"/></returns>
    public bool getTurn()
    {

        return currentTurn;

    }

}
