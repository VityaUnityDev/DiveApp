using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountMoneyCommand : AbstractCommand
{
    public float amount;

    private float CasinoAmount;
    private int percentForCasino = 10;

    public override void Execute()
    {
        amount = GameInfo.Bet *
                 GameInfo.PlayersInCurrentGame.Count;


        if (GameInfo.PlayersInCurrentGame.Count > 1)
        {
            var fees = amount * percentForCasino / 100;
            CasinoAmount += fees;
            GameInfo.OnGetFees(CasinoAmount);
            var result = amount - fees;
            GameInfo.Result = result;
            if (GameInfo.CountWinner > 1)
            {
                GameInfo.Result = result / GameInfo.CountWinner;
            }
        }
    }
}