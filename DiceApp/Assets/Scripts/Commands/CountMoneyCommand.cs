using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountMoneyCommand : AbstractCommand
{
    private float _casinoAmount;
    private int percentForCasino = 10;

    public override void Execute()
    {
      var  amount = GameInfo.Bet *
                 GameInfo.PlayersInCurrentGame.Count;


        if (GameInfo.PlayersInCurrentGame.Count > 1)
        {
            var fees = amount * percentForCasino / 100;
            _casinoAmount += fees;
            GameInfo.OnGetFees(_casinoAmount);
            var result = amount - fees;
            GameInfo.Result = result;
            if (GameInfo.CountWinner > 1)
            {
                GameInfo.Result = result / GameInfo.CountWinner;
            }
        }
    }
    
}