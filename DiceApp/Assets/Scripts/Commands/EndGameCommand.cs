using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCommand : MonoBehaviour
{
   
    public void Execute(int winNumber)
    {
        int countWinner = 0;
        for (int i = 0; i < GameInfo.Players.Count; i++)
        {
            if (GameInfo.Players[i].diceCount == winNumber)
            {
                GameInfo.Players[i].IsWinner = true;
                countWinner++;
            }
        }
        GameInfo.CountWinner = countWinner;
        Debug.Log("Count winner" + countWinner);
        
    }
    
   
    
    
    
    
}
