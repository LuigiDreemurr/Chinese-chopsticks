using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by MATT
/// <para>
/// A data tree to hold and help rank possible moves.
/// Needs to count its own value and that of its next
/// </para>
/// <para>
/// ITS WORKING!!!
/// ...
/// I cant win anymore...
/// </para>
/// </summary>

public class AITree
{

    /// <summary>Current level p1 holder</summary>
    public PlayerClass p1Holder = new PlayerClass("Temp1");
    /// <summary>Current level p1 holder</summary>
    public PlayerClass p2Holder = new PlayerClass("Temp2");

    /// <summary>Holds all possible branches for the next move</summary>
    public List<AITree> possibleMoves = new List<AITree>();
    /// <summary>A multipurpos int, used as a holder in makeLevel and possibly for other testing and bestMove</summary>
    private int currentLevel;
    /// <summary>Int to hold current and future values</summary>
    private int moveValue;

    /// <summary>
    /// empty constructor
    /// </summary>
    public AITree()
    {

    }

    /// <summary>
    /// deconstructor
    /// </summary>
    ~AITree()
    {

    }

    /// <summary>
    /// Main funtion for AI decision making using moveValue
    /// <para>
    /// Currently carries out possibleMove attack/split then
    /// checks 1v2, 2v1, 1v1, 2v2, win (0v2/0v1)
    /// </para>
    /// </summary>
    private void determinValue() //funtion to determin value of each possible move
    {

        for (int i = 0; i < possibleMoves.Count; i++) //need to test each possible move
        {

            attackHand(i);
            //need to test each action and see what may happen...
            //Prefrebly will check next turn as well, but need to
            //avoid infinite future check.

            //11-11 = 0/neutral

            //04-01 = -1000/loose
            //01-04 = -1000/loose
            //03-01 = possible win possible neutral (03-04 || 12-01)
            //01-03 = 100/win

            //if bothHands
            //if single hands
            //if 1v2
            if (!oneOrTwo(possibleMoves[i].p1Holder.getHand('L'), possibleMoves[i].p1Holder.getHand('R')) &&
                oneOrTwo(possibleMoves[i].p2Holder.getHand('L'), possibleMoves[i].p2Holder.getHand('R')))
            {

                int moveTemp = 20;
                char p1H = onlyHand(possibleMoves[i].p1Holder.getHand('L'), possibleMoves[i].p1Holder.getHand('R'));
                //char p2H = onlyHand(possibleMoves[i].p2Holder.getHand('L'), possibleMoves[i].p2Holder.getHand('R'));

                if (possibleMoves[i].p1Holder.getHand(p1H) == 4)
                    moveTemp = 50;
                else
                    moveTemp = 20;

                if (possibleMoves[i].p2Holder.getHandPicked() == 'S') //so if hand would split
                {

                    if (p1Holder.getHand(p1H) + p2Holder.getHand(onlyHand(p2Holder.getHand('L'), p2Holder.getHand('R'))) > 4) //if could kill instead
                    {
                        moveTemp = -20;
                    }
                    else if ((p1Holder.getHand(p1H) + p2Holder.getHand(onlyHand(p2Holder.getHand('L'), p2Holder.getHand('R')))) +
                        p2Holder.getHand(onlyHand(p2Holder.getHand('L'), p2Holder.getHand('R'))) > 4) //if AI cant survive next hit
                    {
                        moveTemp = 60;
                    }

                }

                possibleMoves[i].moveValue = moveTemp;

            }

            //if 2v1
            if (oneOrTwo(possibleMoves[i].p1Holder.getHand('L'), possibleMoves[i].p1Holder.getHand('R')) &&
                !oneOrTwo(possibleMoves[i].p2Holder.getHand('L'), possibleMoves[i].p2Holder.getHand('R')))
            {

                int moveTemp = 0;
                //char p1H = 'L';
                char p2H = onlyHand(possibleMoves[i].p2Holder.getHand('L'), possibleMoves[i].p2Holder.getHand('R'));

                if (possibleMoves[i].p1Holder.getHand('L') + possibleMoves[i].p2Holder.getHand(p2H) > 4 ||
                    possibleMoves[i].p1Holder.getHand('R') + possibleMoves[i].p2Holder.getHand(p2H) > 4) //if any hand can kill AI
                {

                    moveTemp = -100;

                }                    
                else
                    moveTemp = -10;
                
                possibleMoves[i].moveValue = moveTemp;

            }

            //if 1v1
            if (!oneOrTwo(possibleMoves[i].p1Holder.getHand('L'), possibleMoves[i].p1Holder.getHand('R')) &&
                !oneOrTwo(possibleMoves[i].p2Holder.getHand('L'), possibleMoves[i].p2Holder.getHand('R')))
            {

                int moveTemp = 0;
                char p1H = onlyHand(possibleMoves[i].p1Holder.getHand('L'), possibleMoves[i].p1Holder.getHand('R'));
                char p2H = onlyHand(possibleMoves[i].p2Holder.getHand('L'), possibleMoves[i].p2Holder.getHand('R'));

                //killed
                if (possibleMoves[i].p1Holder.getHand(p1H) + possibleMoves[i].p2Holder.getHand(p2H) > 4)
                    moveTemp = -100; //death
                //enemy is 1(cant split) and AI is 3 then kill
                else if (possibleMoves[i].p1Holder.getHand(p1H) == 1 && possibleMoves[i].p2Holder.getHand(p2H) == 3)
                    moveTemp = 90; //win
                //nothing
                else// if (possibleMoves[i].p1Holder.getHand(p1H) == 1 && possibleMoves[i].p2Holder.getHand(p2H) < 3)
                    moveTemp = 0;

                if (p1Holder.getHand('L') + p2Holder.getHand(p2H) > 4 &&
                   p1Holder.getHand('R') + p2Holder.getHand(p2H) > 4) //if BOTH hands can kill AI
                {

                    moveTemp = -200;

                }
                //if only 1 hand can kill and AI can take it out
                else if (p1Holder.getHand(highestHand(p1Holder.getHand('L'), p1Holder.getHand('R'))) + p2Holder.getHand(p2H) > 4)
                {

                    moveTemp = 70;

                }

                possibleMoves[i].moveValue = moveTemp;

            }

            //if 2v2
            if (oneOrTwo(possibleMoves[i].p1Holder.getHand('L'), possibleMoves[i].p1Holder.getHand('R')) &&
                oneOrTwo(possibleMoves[i].p2Holder.getHand('L'), possibleMoves[i].p2Holder.getHand('R')))
            {

                int moveTemp = 0;

                if (possibleMoves[i].p2Holder.getHandPicked() == 'S') //so if hand would split
                {

                    if (p1Holder.getHand(highestHand(p1Holder.getHand('L'), p1Holder.getHand('R')))
                        + p2Holder.getHand(onlyHand(p2Holder.getHand('L'), p2Holder.getHand('R'))) > 4) //if could kill instead
                    {
                        moveTemp = -20;
                    }
                    else if ((p1Holder.getHand(highestHand(p1Holder.getHand('L'), p1Holder.getHand('R')))
                        + p2Holder.getHand(onlyHand(p2Holder.getHand('L'), p2Holder.getHand('R')))) +
                        p2Holder.getHand(onlyHand(p2Holder.getHand('L'), p2Holder.getHand('R'))) > 4) //if AI cant survive next hit
                    {
                        moveTemp = 60;
                    }

                }
                else
                {

                    if (possibleMoves[i].p1Holder.getHand(highestHand(possibleMoves[i].p1Holder.getHand('L'), 
                        possibleMoves[i].p1Holder.getHand('R'))) + 
                        possibleMoves[i].p2Holder.getHand(highestHand(possibleMoves[i].p2Holder.getHand('L'), 
                        possibleMoves[i].p2Holder.getHand('R'))) > 4) //Highest p1 + highest p2 = p2 loss then -20
                    {

                        moveTemp = -20;

                    }

                }

                possibleMoves[i].moveValue = moveTemp;

            }

            //if win
            if (possibleMoves[i].p1Holder.getHand('L') == 0 && possibleMoves[i].p1Holder.getHand('R') == 0)
            {

                possibleMoves[i].moveValue = 1000;

            }

        }

    }

    /// <summary>
    /// Returns char of highest hand
    /// </summary>
    /// <param name="LHand"></param>
    /// <param name="RHand"></param>
    /// <returns>Returns char 'L' or 'R'</returns>
    private char highestHand(int LHand, int RHand)
    {

        if (LHand > RHand)
            return 'L';
        else
            return 'R';

    }

    /// <summary>
    /// Returns char of only remaining hand
    /// </summary>
    /// <param name="L"></param>
    /// <param name="R"></param>
    /// <returns>Returns char 'L' or 'R'</returns>
    private char onlyHand(int L, int R)
    {

        if (L != 0)
            return 'L';
        else
            return 'R';

    }

    /// <summary>
    /// if true then 2
    /// </summary>
    /// <param name="num1"></param>
    /// <param name="num2"></param>
    /// <returns>Returns a bool if there is 1 or 2 hands</returns>
    private bool oneOrTwo(int num1, int num2)
    {

        if (num1 == 0 || num2 == 0)
            return false;
        else
            return true;

    }

    /// <summary>
    /// Handles the possible move attack and/or split. Sets the move for it.
    /// </summary>
    /// <param name="moveChoice"></param>
    private void attackHand(int moveChoice)
    {

        if (possibleMoves[moveChoice].p2Holder.getHandPicked() == 'S')
        {

            possibleMoves[moveChoice].p2Holder.splitHand();

        }
        else
        {

            possibleMoves[moveChoice].p1Holder.addToHand(
                possibleMoves[moveChoice].p1Holder.getHandPicked(),
                    possibleMoves[moveChoice].p2Holder.getHand(
                        possibleMoves[moveChoice].p2Holder.getHandPicked()));

        }

    }

    /// <summary>
    /// creates next move (if possible)
    /// </summary>
    /// <param name="p1In"></param>
    /// <param name="p2In"></param>
    public void makeLevel(PlayerClass p1In, PlayerClass p2In)
    {

        //1-3
        if (p1In.getHand('L') != 0 && p2In.getHand('L') != 0) //left hits left
        {
                        
            possibleMoves.Add(new AITree());

            currentLevel = possibleMoves.Count - 1;
                        
            possibleMoves[currentLevel].p1Holder.classInfoCopy(p1In);
            possibleMoves[currentLevel].p1Holder.setHandPicked('L');

            possibleMoves[currentLevel].p2Holder.classInfoCopy(p2In);
            possibleMoves[currentLevel].p2Holder.setHandPicked('L');

        }

        //1-4
        if (p1In.getHand('L') != 0 && p2In.getHand('R') != 0) //right hits left
        {

            possibleMoves.Add(new AITree());

            currentLevel = possibleMoves.Count - 1;

            possibleMoves[currentLevel].p1Holder.classInfoCopy(p1In);
            possibleMoves[currentLevel].p1Holder.setHandPicked('L');

            possibleMoves[currentLevel].p2Holder.classInfoCopy(p2In);
            possibleMoves[currentLevel].p2Holder.setHandPicked('R');

        }

        //2-3
        if (p1In.getHand('R') != 0 && p2In.getHand('L') != 0) //left hits right
        {

            possibleMoves.Add(new AITree());

            currentLevel = possibleMoves.Count - 1;

            possibleMoves[currentLevel].p1Holder.classInfoCopy(p1In);
            possibleMoves[currentLevel].p1Holder.setHandPicked('R');

            possibleMoves[currentLevel].p2Holder.classInfoCopy(p2In);
            possibleMoves[currentLevel].p2Holder.setHandPicked('L');

        }

        //3-4
        if (p1In.getHand('R') != 0 && p2In.getHand('R') != 0) //right hits right
        {

            possibleMoves.Add(new AITree());

            currentLevel = possibleMoves.Count - 1;

            possibleMoves[currentLevel].p1Holder.classInfoCopy(p1In);
            possibleMoves[currentLevel].p1Holder.setHandPicked('R');

            possibleMoves[currentLevel].p2Holder.classInfoCopy(p2In);
            possibleMoves[currentLevel].p2Holder.setHandPicked('R');

        }

        //p1split         //Might not be needed?
        //p2split
        if ((p2Holder.getHand('L') == 0 && p2Holder.getHand('R') > 1) || (p2Holder.getHand('R') == 0 && p2Holder.getHand('L') > 1))
        {

            possibleMoves.Add(new AITree());

            currentLevel = possibleMoves.Count - 1;

            possibleMoves[currentLevel].p2Holder.setHandPicked('S');  //'S' will be used for split, may have to implement checks elsewhere

        }

        determinValue();

    }

    /// <summary>
    /// Just a faster way to set both PlayerClass holders
    /// </summary>
    /// <param name="p1In"></param>
    /// <param name="p2In"></param>
    public void setPlayers(PlayerClass p1In, PlayerClass p2In)
    {

        p1Holder.classInfoCopy(p1In);
        p2Holder.classInfoCopy(p2In);

    }

    /// <summary>
    /// returns current value
    /// </summary>
    /// <returns>Returns moveValue</returns>
    int getMoveValue()
    {

        return moveValue;

    }

    /// <summary>
    /// returns the next move with the best score L1
    /// </summary>
    public int getBestMove()
    {
        
        int bestMove = 0;

        for (int i = 0; i < possibleMoves.Count; i++)
        {

            if (possibleMoves[i].getMoveValue() > possibleMoves[bestMove].getMoveValue())
                bestMove = i;

        }/*
        Debug.Log("last move val: " + possibleMoves[bestMove].getMoveValue());
        Debug.Log("best move #: " + bestMove); //12,22, makes 14 10, 2v1 error*/
        return bestMove;

    }

}
