using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCommand : MonoBehaviour
{
    public void Execute(int winNumber, int fromCountNumber)
    {
        int countWinner = 0;
        for (int i = fromCountNumber; i < GameInfo.Players.Count; i++)
        {
            if (GameInfo.Players[i]._playerModel.DiceCount == winNumber)
            {
                GameInfo.Players[i]._playerModel.IsWinner = true;
                countWinner++;
            }
        }

        GameInfo.CountWinner = countWinner;
        Debug.Log("Count winner" + countWinner);
    }
}