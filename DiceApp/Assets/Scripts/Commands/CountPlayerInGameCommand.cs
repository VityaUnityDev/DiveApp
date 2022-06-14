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
            GameInfo.Players.ElementAt(i).Value._playerModel.SolutionAboutGame();
            if (GameInfo.Players.ElementAt(i).Value._playerModel.iAgreeWithBet)
            {
                var gamer = GameInfo.Players.ElementAt(i);
                GameInfo.PlayersInCurrentGame.Add(gamer.Key, gamer.Value);
            }
        }
    }
}