using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoFees : MonoBehaviour
{
    public int amount;
    private float CasinoAmount;
    private int percentForCasino = 5;

    public void AmountForPlayer()
    {
        amount = GameInfo.Bet * GameInfo.Players.Count;
        var fees = amount * percentForCasino / 100;
        CasinoAmount += fees;
        var result = amount - fees;
        Debug.Log(result);

        if (GameInfo.CountWinner > 1)
        {
            GameInfo.Result = result / GameInfo.CountWinner;
        }
    }
}