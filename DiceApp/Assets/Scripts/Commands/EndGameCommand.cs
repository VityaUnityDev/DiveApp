using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EndGameCommand : AbstractCommand
{
    public override void Execute()
    {
        int countWinner = 0;
        foreach (var player in GameInfo.PlayersInCurrentGame.Values.ToArray())
        {
            if (player._playerModel.DiceCount == GameInfo.winnerNumber)
            {
                player._playerModel.IsWinner = true;
                countWinner++;
            }
        }

        GameInfo.CountWinner = countWinner;
    }
}