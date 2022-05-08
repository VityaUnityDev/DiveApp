using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandOutMoneyCommand : MonoBehaviour
{
    public void Execute()
    {
        for (int i = 0; i < GameInfo.Players.Count; i++)
        {
            if (GameInfo.Players[i].IsWinner)
            {
                GameInfo.Players[i].SetWinner(GameInfo.Result);
                GameInfo.Players[i].IsWinner = false;

            }
            else
            {
                GameInfo.Players[i].SetLoser(GameInfo.Bet);
            }
        }
        
    }

}
