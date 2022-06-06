using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountMoneyCommand : MonoBehaviour
{
    public int amount;
    private float CasinoAmount;
    private int percentForCasino = 10;

    public void Execute()
    {
        if (GameInfo.MadeBet)
        {
            amount = GameInfo.Bet * GameInfo.Players.Count;
           
        }
        else
        {
            amount = GameInfo.Bet * Mathf.Abs(GameInfo.Players.Count - 1);
        }
        var fees = amount * percentForCasino / 100;
        CasinoAmount += fees;
        GameInfo.OnGetFees((int)CasinoAmount);
        var result = amount - fees;

      
        

        if (GameInfo.CountWinner > 1)
        {
            GameInfo.Result = result / GameInfo.CountWinner;
        }
        else
        {
            GameInfo.Result = result;
        }
    }
}