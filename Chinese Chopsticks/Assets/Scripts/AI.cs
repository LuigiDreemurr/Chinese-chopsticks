using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by MATT
/// <para>
/// Script for AI player and behavior.
/// </para>
/// going to need: 
/// Tree states/private future "board" to test future moves.
/// <para>
/// Decision making for value of moves. 
/// making move based off of value.
/// </para>
/// <para>
/// ******************************
/// </para>
/// ended up doing most of this in AITree and this script 
/// isn't really needed anymore though 
/// <para>
/// I'll leave it for notes/visable progress and in case of future better AI
/// </para>
/// </summary>

public class AI// : MonoBehaviour
{

    //private BaseGame gameController; //link to base game and how the AI move will be done

    public List<AITree> aiTree = new List<AITree>();
    /*
    // Start is called before the first frame update
    void Start()
    {

        PlayerPrefs.SetString("Player2Name", "AI"); //I delayed the initial load in baseGame for this part alone
        gameController = this.GetComponent<BaseGame>(); //loads in the active baseGame script on the same object
        aiTree = new List<AITree>();

    }
    */
    /// <Notes>
    /// Notes 1,2,3,4 used for hand choice.
    /// Needs to pick what will kill the hand.
    /// needs to split if it will be killed next turn.
    /// needs to avoid getting cornered.
    /// 0 is current game state. 1 is next AI move.
    /// Work on 1 turn first then move forward.
    /// </Notes>

    /// <summary>
    /// Will use predicted turn from AIFutureTurn()
    /// </summary>
    private void AITurn()
    {

        //gameController.handButton(aiTree[1].p1Holder.getNumHandPicked()); //hand to attack
        //gameController.handButton(aiTree[1].p2Holder.getNumHandPicked()); //attacking hand

    }

    /// <summary>
    /// will be used to pick the best move and load the hands for it to be used in AITurn()
    /// </summary>
    private void AIFutureTurn()
    {

        aiTree[0].makeLevel(aiTree[0].p1Holder, aiTree[0].p2Holder);

        aiTree[1].setPlayers(aiTree[0].possibleMoves[aiTree[0].getBestMove()].p1Holder, 
            aiTree[0].possibleMoves[aiTree[0].getBestMove()].p2Holder);
        /*
        if (aiTree[1].p1Holder.getHandPicked() == 'L')
            aiTree[1].p1Holder.setNumHandPicked(1);
        else
            aiTree[1].p2Holder.setNumHandPicked(3);

        if (aiTree[1].p2Holder.getHandPicked() == 'L')
            aiTree[1].p1Holder.setNumHandPicked(2);
        else
            aiTree[1].p2Holder.setNumHandPicked(4);
            */
        //aiTree[0].setPlayerHandsPicked(aiTree[0].possibleMoves[aiTree[0].getBestMove()].p1Holder.getHandPicked(),
        //    aiTree[0].possibleMoves[aiTree[0].getBestMove()].p2Holder.getHandPicked());

    }
    /*
    // Update is called once per frame
    void Update()
    {
        
        if (gameController.getTurn() && !gameController.getGameOver()) //then AI turn
        {
            
            aiTree.Clear();
            aiTree.Add(new AITree());
            aiTree[0].setPlayers(gameController.player1, gameController.player2);
            aiTree.Add(new AITree());

            AIFutureTurn();
            AITurn();

        }

    }
    */

    /// <summary>
    /// Calls all needed funtions, yes it has a bad name
    /// </summary>
    /// <param name="p1In"></param>
    /// <param name="p2In"></param>
    public void AIDoAll(PlayerClass p1In, PlayerClass p2In)
    {

        aiTree.Clear();
        aiTree.Add(new AITree());
        aiTree[0].setPlayers(p1In, p2In); //gameController.player1, gameController.player2);
        aiTree.Add(new AITree());

        AIFutureTurn();
        AITurn();

        //Debug.Log("Get best move # " + aiTree[0].getBestMove());

    }

}
