using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandOutMoneyCommand : AbstractCommand
{
    public override void Execute()
    {
        for (int i = 0; i < GameInfo.PlayersInCurrentGame.Values.Count; i++)
        {
            UpdatePlayerInfo(GameInfo.PlayersInCurrentGame.ElementAt(i).Value);
            
            if (GameInfo.PlayersInCurrentGame.ElementAt(i).Value._playerModel.TheEnd)
            {
                GameInfo.PlayersInCurrentGame.ElementAt(i).Value.playerPresenter.EndGame();
                var pl = GameInfo.Players.Keys.ElementAt(i);
                GameInfo.PlayersInCurrentGame.Remove(pl);
                GameInfo.Players.Remove(pl);
                i--;
                PlayerWonGame();
            }
        }


        GameInfo.PlayersInCurrentGame.Clear();
        GameInfo.finishGame = true;
    }

    private void UpdatePlayerInfo(Player player)
    {
        if (player._playerModel.IsWinner)
        {
            player._playerModel.SetWinner(GameInfo.Result);
        }
        else
        {
            player._playerModel.SetLoser();
        }

        player.playerPresenter.ChangeMoney();
    }

    private void PlayerWonGame()
    {
        if (GameInfo.PlayersInCurrentGame.Count == 1)
        {
            var player = GameInfo.PlayersInCurrentGame.Values.ElementAt(0);
            GameInfo.OnGetWinner(player);
            player.playerPresenter.EndGame();
            GameInfo.PlayersInCurrentGame.Clear();
            GameInfo.Players.Clear();
            
        }
    }
}