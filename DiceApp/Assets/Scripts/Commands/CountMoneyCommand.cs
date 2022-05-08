using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountMoneyCommand : MonoBehaviour
{
    public int amount;
    private float CasinoAmount;
    private int percentForCasino = 5;

    public void Execute()
    {
        amount = GameInfo.Bet * GameInfo.Players.Count;
        var fees = amount * percentForCasino / 100;
        CasinoAmount += fees;
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