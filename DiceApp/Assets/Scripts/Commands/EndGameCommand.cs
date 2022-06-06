using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCommand : MonoBehaviour
{
   
    public void Execute(int winNumber)
    {
        int countWinner = 0;
        if (GameInfo.MadeBet == false)
        {
            for (int i = 1; i < GameInfo.Players.Count; i++)
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
        else
        {
            for (int i = 0; i < GameInfo.Players.Count; i++)
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
    
   
    
    
    
    
}
