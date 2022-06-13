using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountMoneyCommand : MonoBehaviour
{
    public float amount;
    private float CasinoAmount;
    private int percentForCasino = 10;

    public void Execute(int fromCountNumber)
    {
        amount = GameInfo.Bet *
                 Mathf.Abs(GameInfo.Players.Count - fromCountNumber); //отнимем 0 или 1 тем самым узнаем сколько игроков

        var fees = amount * percentForCasino / 100;
        CasinoAmount += fees;
        GameInfo.OnGetFees(CasinoAmount);
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