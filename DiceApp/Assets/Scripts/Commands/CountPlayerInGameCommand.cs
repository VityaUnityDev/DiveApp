using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CountPlayerInGameCommand : AbstractCommand
{
    public override void Execute()
    {
        for (int i = 1; i < GameInfo.Players.Values.Count; i++)
        {
            var player = GameInfo.Players.ElementAt(i).Value._playerModel;
            
            player.SolutionAboutGame();
            if (player.IAgreeWithBet)
            {
                var currentPlayer = GameInfo.Players.ElementAt(i);
                GameInfo.PlayersInCurrentGame.Add(currentPlayer.Key, currentPlayer.Value);
            }
        }
    }
}